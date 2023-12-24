using System;
using System.IO;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string xmlFilePath = Server.MapPath("/db/data.xml");

        XmlDocument xmldoc = new XmlDocument();

        if (File.Exists(xmlFilePath))
        {
            xmldoc.Load(xmlFilePath);
            reparticles.DataSource = xmldoc.SelectSingleNode("articles").ChildNodes;
            reparticles.DataBind();
        }

    }
}