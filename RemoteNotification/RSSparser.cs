using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RemoteNotification
{

    public class RSSparser
    {
        //XNamespace nsYandex = "http://news.yandex.ru";
        
        
        // Класс который отвечает за настройки канала
        public class ChannelClass
        {
            public string title;
            public string description;
            public string link;
            public string copyright;
            public string names;

            public ChannelClass()
            {
                title = "";
                description = "";
                link = "";
                copyright = "";
                names = "";
            }
        }

        public class ImageOfChanel
        {
            public string imgTitle;
            public string imgLink;
            public string imgURL;

            public ImageOfChanel()
            {
                imgTitle = "";
                imgLink = "";
                imgURL = "";
            }
        }

        public class Items
        {
            public string title;
            public string link;
            public string description;
            public string pubDate;
            public string names;
            

            public Items()
            {
                title = "";
                link = "";
                description = "";
                pubDate = "";
                names = "";
            }
        }

        public ImageOfChanel imageChanel = new ImageOfChanel(); // обьект класса рисунка
        public Items[] articles; // создаем массив элементов item канала
        public ChannelClass channel = new ChannelClass(); // создаем обьект класса ChannelClass


       public bool getNewArticles(string fileSource)
        {
            try
            {
                
                XmlDocument doc = new XmlDocument();
                doc.Load(fileSource);
                
                XmlNodeList nodeList;
                XmlNode root = doc.DocumentElement;
                articles = new Items[root.SelectNodes("channel/item").Count];
                nodeList = root.ChildNodes;
                int count = 0;

                foreach (XmlNode chanel in nodeList)
                {
                    foreach (XmlNode chanel_item in chanel)
                    {

                        if (chanel_item.Name == "title")
                        {
                            channel.title = chanel_item.InnerText;
                        }
                        if (chanel_item.Name == "description")
                        {
                            channel.description = chanel_item.InnerText;
                        }
                        if (chanel_item.Name == "copyright")
                        {
                            channel.copyright = chanel_item.InnerText;
                        }
                        if (chanel_item.Name == "link")
                        {
                            channel.link = chanel_item.InnerText;
                        }

                        if (chanel_item.Name == "img")
                        {
                            XmlNodeList imgList = chanel_item.ChildNodes;
                            foreach (XmlNode img_item in imgList)
                            {
                                if (img_item.Name == "url")
                                {
                                    imageChanel.imgURL = img_item.InnerText;
                                }
                                if (img_item.Name == "link")
                                {
                                    imageChanel.imgLink = img_item.InnerText;
                                }
                                if (img_item.Name == "title")
                                {
                                    imageChanel.imgTitle = img_item.InnerText;
                                }
                            }
                        }

                        if (chanel_item.Name == "item")
                        {
                            XmlNodeList itemsList = chanel_item.ChildNodes;
                            articles[count] = new Items();

                            foreach (XmlNode item in itemsList)
                            {
                                channel.names += item.Name + " ";
                                if (item.Name == "title")
                                {
                                    articles[count].title = item.InnerText;
                                }
                                if (item.Name == "link")
                                {
                                    articles[count].link = item.InnerText;
                                }
                                if (item.Name == "yandex:full-text")
                                {
                                    articles[count].description = item.InnerText;
                                }
                                if (item.Name == "pubDate")
                                {
                                    articles[count].pubDate = item.InnerText;
                                }
                            }
                            count += 1;
                        }


                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}