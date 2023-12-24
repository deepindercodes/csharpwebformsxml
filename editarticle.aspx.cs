using System;
using System.Xml;

public partial class editarticle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Int32 id = Convert.ToInt32(Request["id"]);

            string xmlFilePath = Server.MapPath("/db/data.xml");

            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(xmlFilePath);

            var objArticle = xmldoc.SelectSingleNode("articles").SelectSingleNode("article[@id=" + id + "]");

            txtarticletitle.Text = objArticle.SelectSingleNode("articletitle").InnerText;
            txtarticleauthor.Text = objArticle.SelectSingleNode("articleauthor").InnerText;
            txtarticlebody.Text = objArticle.SelectSingleNode("articlebody").InnerText;
            hdnarticleimage.Value = objArticle.SelectSingleNode("articleimage").InnerText;

        }


    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Int32 id = Convert.ToInt32(Request["id"]);

        string articletitle = txtarticletitle.Text;
        string articleauthor = txtarticleauthor.Text;
        string articlebody = txtarticlebody.Text;
        string articleimage = hdnarticleimage.Value;

        string xmlFilePath = Server.MapPath("/db/data.xml");

        XmlDocument xmldoc = new XmlDocument();

        xmldoc.Load(xmlFilePath);

        var objArticle = xmldoc.SelectSingleNode("articles").SelectSingleNode("article[@id=" + id + "]");

        objArticle.SelectSingleNode("articletitle").ChildNodes[0].InnerText = articletitle;
        objArticle.SelectSingleNode("articleauthor").ChildNodes[0].InnerText = articleauthor;
        objArticle.SelectSingleNode("articlebody").ChildNodes[0].InnerText = articlebody;
        objArticle.SelectSingleNode("articleimage").ChildNodes[0].InnerText = articleimage;
        objArticle.SelectSingleNode("modifiedonutc").ChildNodes[0].InnerText = DateTime.UtcNow.ToString();

        xmldoc.Save(xmlFilePath);

        Response.Write("<script type='text/javascript'>parent.ArticleEdited();</script>");
        Response.End();
    }
}