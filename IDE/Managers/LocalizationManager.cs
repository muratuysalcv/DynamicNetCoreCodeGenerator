using Scriptingo.Admin.Models;
using Scriptingo.Common;
using Scriptingo.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scriptingo.Admin
{
    public class LocalizationManager
    {
        public string GetResource(string word, string lang)
        {
            
            var db = new FastApiContext<_word>();
            var item = db.Data.FirstOrDefault(x => x.word == word && x.language == lang);
            if (item != null)
            {
                return item.value;
            }
            else
            {
                var langs = new FastApiContext<_lang>().Data.ToList();
                foreach (var itemLang in langs)
                {
                    if (db.Data.Count(x => x.word == word && x.language == itemLang.culture_code) == 0)
                    {
                        db.Data.Add(new _word()
                        {
                            word = word,
                            language = itemLang.culture_code,
                            value = word
                        });
                    }
                }
                db.SaveChanges();
            }
            return word;
        }
    }
}
