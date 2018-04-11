using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace WCFAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IIsgecServices" in both code and config file together.
    [ServiceContract]
    public interface IIsgecServices
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "ProjectCode")
          ]
        List<ServicesClass> ProjectCode();
    }
}
