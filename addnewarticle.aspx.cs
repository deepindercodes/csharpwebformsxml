using System;
using System.IO;
using System.Xml;

public partial class addnewarticle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        diverror.Visible = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string articletitle = txtarticletitle.Text;
        string articleauthor = txtarticleauthor.Text;
        string articlebody = txtarticlebody.Text;
        string articleimage = hdnarticleimage.Value;
        bool articleExists = false;

        string xmlFilePath = Server.MapPath("/db/data.xml");

        Int32 pk = 1;

        XmlDocument xmldoc = new XmlDocument();


        if (File.Exists(xmlFilePath))
        {
            xmldoc.Load(xmlFilePath);

            pk = Convert.ToInt32(xmldoc.DocumentElement.GetAttribute("pk")) + 1;

            xmldoc.DocumentElement.SetAttribute("pk", (pk).ToString());
        }
        else
        {
            var objPI = xmldoc.CreateProcessingInstruction("xml", "version='1.0'");
            xmldoc.InsertBefore(objPI, xmldoc.ChildNodes[0]);

            var objRoot = xmldoc.CreateElement("articles");

            var objpk = xmldoc.CreateAttribute("pk");

            objpk.Value = pk.ToString();
            objRoot.SetAttributeNode(objpk);

            xmldoc.AppendChild(objRoot);

        }

        var objParent = xmldoc.DocumentElement;

        //adding the article node
        var objnewArticle = xmldoc.CreateElement("article");

        //setting id
        var objid = xmldoc.CreateAttribute("id");
        objid.InnerText = (pk) + "";
        objnewArticle.SetAttributeNode(objid);

        //setting article title
        var objarticletitle = xmldoc.CreateElement("articletitle");
        var objarticletitletext = xmldoc.CreateCDataSection(articletitle);
        objarticletitle.AppendChild(objarticletitletext);
        objnewArticle.AppendChild(objarticletitle);

        //setting article author
        var objarticleauthor = xmldoc.CreateElement("articleauthor");
        var objarticleauthortext = xmldoc.CreateCDataSection(articleauthor);
        objarticleauthor.AppendChild(objarticleauthortext);
        objnewArticle.AppendChild(objarticleauthor);

        //setting article body
        var objarticlebody = xmldoc.CreateElement("articlebody");
        var objarticlebodytext = xmldoc.CreateCDataSection(articlebody);
        objarticlebody.AppendChild(objarticlebodytext);
        objnewArticle.AppendChild(objarticlebody);

        //setting article image
        var objarticleimage = xmldoc.CreateElement("articleimage");
        var objarticleimagetext = xmldoc.CreateCDataSection(articleimage);
        objarticleimage.AppendChild(objarticleimagetext);
        objnewArticle.AppendChild(objarticleimage);

        //setting createdonutc
        var objcreatedonutc = xmldoc.CreateElement("createdonutc");
        var objcreatedonutctext = xmldoc.CreateCDataSection(DateTime.UtcNow.ToString());
        objcreatedonutc.AppendChild(objcreatedonutctext);
        objnewArticle.AppendChild(objcreatedonutc);

        //setting modifiedonutc
        var objmodifiedonutc = xmldoc.CreateElement("modifiedonutc");
        var objmodifiedonutctext = xmldoc.CreateCDataSection("");
        objmodifiedonutc.AppendChild(objmodifiedonutctext);
        objnewArticle.AppendChild(objmodifiedonutc);

        //setting status
        var objstatus = xmldoc.CreateElement("status");
        var objstatustext = xmldoc.CreateCDataSection("E");
        objstatus.AppendChild(objstatustext);
        objnewArticle.AppendChild(objstatus);

        objParent.AppendChild(objnewArticle);

        xmldoc.Save(xmlFilePath);


        Response.Write("<script type='text/javascript'>parent.newArticleAdded();</script>");
        Response.End();

    }
}