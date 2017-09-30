using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine.UI;
/// <summary>
/// 关于XML的读取，好好的看看!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
/// </summary>
public class Tools 
{
    public static List<FileInfo> GetLevelFiles()
    {
        //将对应路径下的XML文件加载到list当中
        string[] files = Directory.GetFiles(Consts.LevelDir, "*.xml");//!!!!!!!!!!!!!!!!!

        List<FileInfo> list = new List<FileInfo>();
        for(int i=0;i<files.Length ;i++)
        {
            FileInfo file = new FileInfo(files[i]);
            list.Add(file);
        }
        return list;
    }

    public static void FillLevel(string fileName,ref Level level)
    {
        //读取XML中的值，然后填满一个Level对象
        FileInfo file = new FileInfo(fileName);
        StreamReader sr = new StreamReader(file.OpenRead(), Encoding.UTF8);

        XmlDocument doc = new XmlDocument();
       
        doc.Load(sr);//将文件信息转化为一个XML格式的文档

        level.Name = doc.SelectSingleNode("/Level/Name").InnerText;
        level.cardImage = doc.SelectSingleNode("/Level/CardImage").InnerText;
        level.Background = doc.SelectSingleNode("/Level/Background").InnerText;
        level.Road = doc.SelectSingleNode("/Level/Road").InnerText;
        level.InitScore = int.Parse(doc.SelectSingleNode("/Level/InitScore").InnerText);

        XmlNodeList nodes;

        nodes = doc.SelectNodes("/Level/Holder/Point");
        for(int i=0;i<nodes.Count ;i++)
        {
            XmlNode node = nodes[i];
            Point p = new Point(int.Parse(node.Attributes["X"].Value), int.Parse(node.Attributes["Y"].Value));//Point有两个属性，一个是x，一个是y

            level.Holder.Add(p);
        }

        nodes = doc.SelectNodes("/Level/Path/Point");
        for(int i=0;i<nodes.Count ;i++)
        {
            XmlNode node = nodes[i];
            Point p = new Point(int.Parse(node.Attributes["X"].Value), int.Parse(node.Attributes["Y"].Value));

            level.Path.Add(p);
        }

        nodes = doc.SelectNodes("/Level/Rounds/Round");
        for(int i=0;i<nodes .Count ;i++)
        {
            XmlNode node = nodes[i];
            Round r = new Round(int.Parse(node.Attributes["Monster"].Value), int.Parse(node.Attributes["Count"].Value));

            level.Rounds.Add(r);
        }


        sr.Close();
        sr.Dispose();

    }


    public static void SaveLevel(string fileName,Level level)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append ("<?xml version=\"1.0\" encoding=\"utf-8\"?>");//注意其中的转意字符！！！
        sb.Append("<Level>");

        sb.AppendLine(string.Format("<Name>{0}</Name>", level.Name));
        sb.AppendLine(string.Format("<Background>{0}</Background", level.Background));
        sb.AppendLine(string.Format("<Road>{0}</Road>", level.Road));
        sb.AppendLine(string.Format("<IniteScore>{0}</IniteScore>",level.InitScore));

        sb.AppendLine("<Hodler>");
        for(int i=0;i<level.Holder .Count ;i++)
        {
            sb.AppendLine (string .Format ("<Point X=\"{0)\" Y=\"{1}\"/>",level.Holder [i].X ,level.Holder [i].Y ));
        }
        sb.AppendLine ("</Hodler>");

        sb.AppendLine("<Path>");
        for (int i = 0; i < level.Path.Count; i++)
        {
            sb.AppendLine(string.Format("<Point X=\"{0}\" Y=\"{1}\"/>", level.Path[i].X, level.Path[i].Y));
        }
        sb.AppendLine("</Path>");

        sb.AppendLine("<Rounds>");
        for (int i = 0; i < level.Rounds.Count; i++)
        {
            sb.AppendLine(string.Format("<Round Monster=\"{0}\" Count=\"{1}\"/>", level.Rounds[i].Monster, level.Rounds[i].Count));
        }
        sb.AppendLine("</Rounds>");

        sb.AppendLine("</Level>");

        string content = sb.ToString();

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.ConformanceLevel = ConformanceLevel.Auto;
        settings.IndentChars = "\t";
        settings.OmitXmlDeclaration = false;

        XmlWriter xw = XmlWriter.Create(fileName, settings);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(content);//变成XML文档
        doc.WriteTo(xw);//写入到目的文件中，并且做一些设定
        xw.Flush();
        xw.Close();

        //StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8);
        //sw.Write(content);
        //sw.Flush();
        //sw.Dispose();
    }

    public static IEnumerator LoadImage(string url,SpriteRenderer render)
    {
        //通过加载的2D纹理，去创建一个SpriteRender
        WWW www = new WWW(url);
        while (!www.isDone)
            yield return www;

        Texture2D texture = www.texture;
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        render.sprite = sp;
        
    }

    public static IEnumerator LoadImage(string url, Image render)
    {
        //通过加载的2D纹理，去创建一个SpriteRender
        WWW www = new WWW(url);
        while (!www.isDone)
            yield return www;

        //加载进来的东西是一个纹理，，利用纹理创建一个精灵（sprite），在赋值给Image.sprite
        Texture2D texture = www.texture; //加载进来的图片是一个2d的纹理
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        render.sprite = sp;
    }
	
}
