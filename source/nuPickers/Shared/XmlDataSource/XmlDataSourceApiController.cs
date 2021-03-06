﻿
namespace nuPickers.Shared.XmlDataSource
{
    using Newtonsoft.Json.Linq;
    using nuPickers.Shared.CustomLabel;
    using nuPickers.Shared.Editor;
    using nuPickers.Shared.TypeaheadListPicker;
    using System.Collections.Generic;
    using System.Web.Http;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;

    [PluginController("nuPickers")]
    public class XmlDataSourceApiController : UmbracoAuthorizedJsonController
    {
        [HttpPost]
        public IEnumerable<EditorDataItem> GetEditorDataItems([FromUri] int contextId, [FromBody] dynamic data)
        {
            XmlDataSource xmlDataSource = ((JObject)data.config.dataSource).ToObject<XmlDataSource>();

            IEnumerable<EditorDataItem> editorDataItems = xmlDataSource.GetEditorDataItems(contextId);

            CustomLabel customLabel = new CustomLabel((string)data.config.customLabel, contextId);
            TypeaheadListPicker typeaheadListPicker = new TypeaheadListPicker((string)data.typeahead);

            // process the labels and then handle any type ahead text
            return typeaheadListPicker.ProcessEditorDataItems(customLabel.ProcessEditorDataItems(editorDataItems));
        }
    }
}
