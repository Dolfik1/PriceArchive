using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace PriceArchive.Parser.Modules
{
    class citilink : SiteModule
    {
        private Dictionary<string, string> _categories = new Dictionary<string, string> 
        { 
            { "Телевизоры", "http://www.citilink.ru/catalog/audio_and_digits/tv/" },
        };

        private string _site = "http://citilink.ru";

        public citilink()
        { 

        }

        public void parse()
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            HtmlNode node = null;

            bool _stopParsing = false;
            foreach (var cat in _categories)
            {
                for (int i = 1; i < 200; i++)
                {
                    if (_stopParsing)
                    {
                        _stopParsing = false;
                        break;
                    }
                    doc = hw.Load(cat.Value + "?p=" + i);
                    for (int e = 1; e <= 20; e++)
                    {
                        node = doc.DocumentNode.SelectSingleNode("//html/body/div[1]/div[5]/div[1]/div[2]/div/table[" + e + "]/tbody/tr[1]/td[1]/a");//name
                        if (node != null)
                        {
                            node.GetAttributeValue("href", null);//url. null - return value if not exist
                            node = doc.DocumentNode.SelectSingleNode("//html/body/div[1]/div[5]/div[1]/div[2]/div/table[" + e + "]/tbody/tr[1]/td[2]/div[1]");//price
                            
                        }
                        else
                        {
                            _stopParsing = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}
