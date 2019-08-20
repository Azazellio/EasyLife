using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using IniParser;
using IniParser.Model;

namespace EasyLife
{
    class Program
    {

        static void Main(string[] args)
        {
            var file_name = "1.ini";  /// оно вродь создает само файл если его нет
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(file_name);  /// указываем какой именно файл нам нужен

            var lady_id = "0000000"; /// эта равна текстбоксу
            var newBlockList = String.Format("1872000{0}32624562{0}32625197{0}32568331{0}32623831", "\r\n");  /// эта переменная просто будет равна текстбоксу
            var oldBlocklist = data["6623235"]["BlockList"];   /// мы должны знать что уже есть в файле

            data["6623235"]["BlockList"] = SmartAdd(oldBlocklist, newBlockList);  /// след две строчки: так работает запись в этой библе (самый норм способ)
            ///data.Sections.AddSection("0000000");
            parser.WriteFile(file_name, data);
            Sectioner(lady_id, file_name);
            ///Console.WriteLine(blocklist);
        }
        public static string SmartAdd(string oldS, string newS)  /// Ne yebi sebe mozg
        {
            string[] oldA = oldS.Split(",");
            string[] newA = newS.Split("\r\n");
            if (Checker(newA) == false)
            {
                Console.WriteLine("Raise an error");   /// тут только один момент. сделай чтоб оно писало красиво мол лист что вы ввели имеет не подходящие айди. Если выдает ошибку, в файл ничего нового не пишется
                return oldS;  /// просто оставляем что было
            }
            List<string> resultL = new List<string>();
            Console.WriteLine(oldA.Length);
            if (oldA.Length == 1 && oldA[0] == "")
            {
                return newS.Replace("\r\n", ",");
            }
            else
            {
                for (int i = 0; i < newA.Length; i++)
                {
                    if (!oldA.Contains(newA[i]))
                    {
                        resultL.Add(newA[i]);
                    }
                }
                for (int i = 0; i < oldA.Length; i++)
                {
                    resultL.Add(oldA[i]);
                }
                string[] resultA = resultL.ToArray();
                foreach (string elem in resultA)
                {
                    Console.WriteLine(elem);
                }
                return string.Join(",", resultA);
            }
        }
        public static bool Checker(string[] newA) ///Check if input is correct. Id is from 7 to 8 digits
        {
            foreach (string member in newA)
            {
                if (member.Length > 8 | member.Length < 7)
                    return false;
            }
            return true;
        }

        public static void Sectioner (string lady_id, string file_name)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(file_name);
            List<string> sectionL = new List<string>();
            foreach (SectionData d in data.Sections)
            {
                sectionL.Add(d.SectionName);
            }
            if (!sectionL.Contains(lady_id))
            {
                Console.WriteLine("no contain");
                data.Sections.AddSection(lady_id);
                var current = data[lady_id];
                current.AddKey("BlockList", "");
                current.AddKey("PauseAfterRequest", "500");
                current.AddKey("SendLimitFrom", "5");
                current.AddKey("SendLimitTo", "15");
                current.AddKey("IsSendTextMsg", "True");
                current.AddKey("IsSendPhoto", "False");
                current.AddKey("IsSendAutoAnswerLike", "False");
                current.AddKey("IsSendAutoAnswerWink", "False");
                current.AddKey("IsSendOnlyPremium", "False");
                current.AddKey("IsSendMoreThanCredits", "False");
                current.AddKey("SendCreditLimit", "20");
                current.AddKey("IsAutoOpenChat", "False");
                current.AddKey("MinuteIgnoreWinkLike", "10");
                current.AddKey("SendingType", "SendAll");
                current.AddKey("BookmarkedAddingWink", "False");
                current.AddKey("BookmarkedAddingLike", "False");
                current.AddKey("IsMirrorVictoriaBrides", "True");
                current.AddKey("IsMirrorVictoriaHearts", "True");
                current.AddKey("IsMirrorValentime", "True");
                current.AddKey("IsMirrorCharmerly", "True");
                current.AddKey("IsMirrorLoveSwans", "True");
                current.AddKey("IsMirrorVictoriaDates", "True");
                current.AddKey("IsMirrorBravoDate", "True");
                current.AddKey("IsMirrorUkrainiancharm", "True");
                current.AddKey("IsMirrorMatchTruly", "True");
                current.AddKey("IsMirrorRondevo", "True");
                current.AddKey("IsMirrorWishDates", "True");
                current.AddKey("IsSendMsgRandom", "True");
                current.AddKey("IsSendMsgQueue", "False");
                current.AddKey("MailBlockList", "");
                current.AddKey("MailRequestDelay", "2000");

                parser.WriteFile(file_name, data);
            }
        }
    }
}


/*
чекни все ли дефолты правильно переписал

[6623235]
BlockList = 
PauseAfterRequest = 500
SendLimitFrom = 5
SendLimitTo = 15
IsSendTextMsg = True
IsSendPhoto = False
IsSendAutoAnswerLike = False
IsSendAutoAnswerWink = False
IsSendOnlyPremium = False
IsSendMoreThanCredits = False
SendCreditLimit = 20
IsAutoOpenChat = False
MinuteIgnoreWinkLike = 10
SendingType = SendAll
BookmarkedAddingWink = False
BookmarkedAddingLike = False
IsMirrorVictoriaBrides = True
IsMirrorVictoriaHearts = True/
IsMirrorValentime = True.
IsMirrorCharmerly = True.
IsMirrorLoveSwans = True.
IsMirrorVictoriaDates = True.
IsMirrorBravoDate = True.
IsMirrorUkrainiancharm = True
IsMirrorMatchTruly = True
IsMirrorRondevo = True
IsMirrorWishDates = True
IsSendMsgRandom = True.
IsSendMsgQueue = False.
MailBlockList = 
MailRequestDelay = 2000
*/