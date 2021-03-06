﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tridion.ContentManager.Templating.Assembly;
using Tridion.ContentManager.CommunicationManagement;
using TCM = Tridion.ContentManager.ContentManagement;
using Dynamic = DD4T.ContentModel;
using DD4T.Templates.Base;
using DD4T.Templates.Base.Builder;

namespace DD4T.Templates
{
    /// <summary>
    /// Adds metadata of the folder containing the component, or one of its parents, to the current component
    /// </summary>
    [TcmTemplateTitle("Add inherited metadata to component")]
    [TcmTemplateParameterSchema("resource:DD4T.Templates.Resources.Schemas.Dynamic Delivery Parameters.xsd")]
    public partial class InheritMetadataComponent : BaseComponentTemplate
    {
        protected Dynamic.MergeAction defaultMergeAction = Dynamic.MergeAction.Skip;
        protected override void TransformComponent(Dynamic.Component component)
        {

            TCM.Component tcmComponent = this.GetTcmComponent();
            TCM.Folder tcmFolder = (TCM.Folder)tcmComponent.OrganizationalItem;

            while (tcmFolder.OrganizationalItem != null)
            {
                if (tcmFolder.MetadataSchema != null)
                {
                    TCM.Fields.ItemFields tcmFields = new TCM.Fields.ItemFields(tcmFolder.Metadata, tcmFolder.MetadataSchema);
                    FieldsBuilder.AddFields(component.MetadataFields, tcmFields, Manager);
                }
                tcmFolder = (TCM.Folder)tcmFolder.OrganizationalItem;
            }

        }
    }
}
