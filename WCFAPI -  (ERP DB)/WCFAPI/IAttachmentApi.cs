using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Services;

namespace WCFAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAttachmentApi" in both code and config file together.
    [ServiceContract]
    public interface IAttachmentApi
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "Attachments/{AthHandleS}/{AthHandleT}/{IndexS}/{IndexT}") //Attachments/{AthHandleS}/{AthHandleT}/{IndexS}/{IndexT}
          ]
        string Attachments(string AthHandleS, string AthHandleT, string IndexS, string IndexT); //
    }
}
