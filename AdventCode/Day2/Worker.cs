using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day2
{
    public class Worker
    {
        private List<Box> _boxes = new List<Box>();

        public Worker()
        {
            var idArray = _ids.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in idArray)
            {
                _boxes.Add(new Box(id));
            }
        }

        public int Work1()
        {
            int doubles = _boxes.Where(b => b.HasDouble).Count();
            int triples = _boxes.Where(b => b.HasTriple).Count();
            return doubles * triples;
        }

        public string Work2()
        {
            foreach (var box in _boxes)
            {
                foreach (var other in _boxes.Skip(_boxes.IndexOf(box) + 1))
                {
                    if (box.Compare(other))
                        return RemoveDiffChar(box.Id, other.Id);
                }
            }

            return "not found";
        }

        private string RemoveDiffChar(string id1, string id2)
        {
            for (int i = 0; i < id1.Length; i++)
            {
                if (id1[i] != id2[i])
                {
                    id1 = id1.Remove(i, 1);
                    return id1;
                }
            }

            return "identical IDs, this should not happen";
        }

        string _ids = @"ovfclbidieyujztrswxmckgnaw
pmfqlbimheyujztrswxmckgnap
ovfqlbidhetujztrswxmcfgnas
gvfqebddheyujztrswxmckgnap
ovfqlbidheyejztrswxqekgnap
ovzqlbiqheyujztsswxmckgnap
oofqlbidhoyujztoswxmckgnap
ovfqlbicqeyujztrswxmckgncp
ovfelbidheyujltrswxmcwgnap
ovfqlbidheyujzerswxmchgnaf
bvfqlbidheyxjztnswxmckgnap
ovfqlbidheyugztrswamnkgnap
ovfqxbidheyujrtrswxmckgbap
ovfqlbidheyujztrdwxqckgjap
ovfqebiqheyujztrscxmckgnap
avfqlbidheyvjztkswxmckgnap
ovfqlbidheyujktrswxvskgnap
ovfqlbidheeujztrswrmckgnae
ovaqlbidheydjztrswxmchgnap
ovfqlbodweyujztpswxmckgnap
xvfqlbidhmyujztrswxmykgnap
ovfqlnidheyujztxswumckgnap
ovfqlbidhexujztrswxyckgeap
ovfqlkidhekubztrswxmckgnap
ovfqlbidheysjzkrsxxmckgnap
oxfqebidheyujzprswxmckgnap
ovfqlbidhetujztrswmmckghap
ovfclbidhuyujztrswrmckgnap
ovfqlbijhdyujztrswxmcvgnap
ovfqlkidhyyujztrswxvckgnap
ovfqlbiehlyujztrswxhckgnap
ovfqlbidheyxjjtrsdxmckgnap
jvfqlbidheyujztrvwxmckcnap
ovfvlbidheyujzhrswxmckgnzp
ovfqnbidhuyujztrswfmckgnap
ovfrlbidheyujztpswxmckgnat
ovfqpbidheyujztrywxmcngnap
ovfqlbidheyumrtrswpmckgnap
ovfqlbidhoyzjztrswxmckgkap
ovfqlbibheyuhztrswxmskgnap
ovfqlbidheybjzfrswxkckgnap
ovfqnbinheyujztrawxmckgnap
ovfqlbidheyujztryxxmckgnao
ovfqzbidheyujztrsuxmckgnpp
ovfqlbidherujztrswxmckgjsp
ovfqlbidheyujhtrywxmckgtap
oofmlbidheyujftrswxmckgnap
ovfqlbidhhyujztrawxmckgnbp
ovfqlbidheyujztrswxeckmnae
lvfqlbidheyujztrswxzckvnap
ovfqlbidheyujztrswxmckqnfr
offqlbidheyrjztrswxmwkgnap
ovnqlbidzeyujztmswxmckgnap
ovfxlbxdheyujztrawxmckgnap
ovfqmbidheyujztrsaxwckgnap
ovfqlbidhryzjztrswxmckgcap
offqlbidheyujnthswxmckgnap
ogmqlbimheyujztrswxmckgnap
ovfqlbidheyulztkswxockgnap
ovfqlbidheyujjtrswxmckypap
ovfqibidheypjztrswxmskgnap
ovfqlbwdhyyujztrswxmckgnnp
ovfqlbidheyujztsvwxmckgkap
odfqlbidoeyujztrswxjckgnap
ovfqlbodpeyujztrswxmcggnap
ovfqlbicheyujztrhwxmcagnap
ovfqlbidheyuaztrgwxmckhnap
ovfwlbidhyyujztrswtmckgnap
ovfqlbidgzyujztrswxmckgcap
ovnqlbcdheyujztrswxmckgnup
ovfqlbieheyujrtrsdxmckgnap
ovfqlbidkeyujztrswfmckgnqp
ovfqlbidtekujztrswxsckgnap
ovfklbedheyujztrscxmckgnap
ovfqltivhnyujztrswxmckgnap
ovfqlbidheyuvuyrswxmckgnap
ovfqlbidheyjjrtrcwxmckgnap
ojfqlbidheyujztrswxmckguvp
ovfqlbidheiqjqtrswxmckgnap
ivfqlfidheyujatrswxmckgnap
cvfqlbidheyujgtrswxmckgnrp
ovfclbidheeujztrswxmckgnaw
ovfqlbhdheyujztrswvmcklnap
ovfqcbidheyvjztaswxmckgnap
ovgqlbijheyujztrswxvckgnap
gvfqlbidheyujvtrswxmckgnaj
ovfqlbidheyujztrdwxmcggnvp
cvfqlbidheyujgtrswxmckqnap
ovfqlbrdheyqjztrswxmckgnaj
ovfqlbidheyujzjrswbmcrgnap
ovfqlbvdheyujxtrswxvckgnap
ovaqlbidheyujctrswxmbkgnap
ovfqlbidheyujztgdwxvckgnap
ovfqlbidhevujztrssxmwkgnap
rvfqlbidheyujztrzwxmckhnap
ovfqmbidheysjztrswxmikgnap
ovfqlbidheiujztrsdxuckgnap
ovfqlbidheyveztrswxmckgnah
ovfqnbiaheytjztrswxmckgnap
ovfqlbidnayujhtrswxmckgnap
ovfqlbidheyujztnswxdckgnag
ovfqlbidheyuyztrswxmzzgnap
ovfqlbiohexujzthswxmckgnap
lvfqlbidheyujztcswxxckgnap
ovuqlbidhfxujztrswxmckgnap
ovfqluidheyujotrswxmrkgnap
ovfalbidheyujztrswxhckgngp
ohjqlbidheyujztrswumckgnap
ovfqxbidhecujztrspxmckgnap
ovfqcbidheyusztrpwxmckgnap
fvfwlbidheyujztrswxmcxgnap
ovfqlbidhxyplztrswxmckgnap
ovfqlbidheyujftrswxdckgrap
ovfqlepdheyujztrswxmckgnjp
ojjqlbidhuyujztrswxmckgnap
ovfqlbwdheyujztrswxmcggeap
ovfqlbidheyujltrscxkckgnap
oifqibidheyujztrswxjckgnap
ovfqlbigheyujztrswdmcqgnap
ovfqlbieheyujztrswxzzkgnap
ovfqlbidheyujztrswmmcgbnap
ovfqlbidhnyujzerswxmkkgnap
ovfqdbinheyujztrswxeckgnap
oveqlbidheyujztrswhmckgnab
ovfqkbytheyujztrswxmckgnap
ovfqlbidheyujstsswxmcklnap
ovfimbidheyujztrewxmckgnap
ovfqebidhequjztrnwxmckgnap
ovfqlbidheyukztrswxmckunwp
oifqlbidheyuwztrswxmckgnao
ovfqlbidweyufztrswxmckgtap
evfqlbidheyujztrswxsckvnap
svbqlbidheyujztrsaxmckgnap
ovfqlbidheyaoztrswxmckjnap
ovfqllidheyujztrswxmckynhp
ohfqlbidheyujatrswtmckgnap
omfjlfidheyujztrswxmckgnap
xvfqlbidheyujutrswxvckgnap
ovfqlbidheyukztsswxmzkgnap
ovfqibidhehujztrswxeckgnap
ovfqlbydheyuoztrswxmcygnap
ovfqlbidheyufztrmwxmckvnap
ovfqrbkdheyujztrswxmckgnaq
ovfqlbigheyuyztrswxmckgzap
ovfqlbidheyujztrsjxmcnnnap
uvfqlbipheyujztrswxmckgnay
ovfqlbddneyujbtrswxmckgnap
tvfqlbidheyujztrswxpckgeap
ovfqlbidheyuiztrhwxmckznap
ovfqlbidheyujzteswxvckgvap
avfqlbidheyijzlrswxmckgnap
oqfqmbidheyujvtrswxmckgnap
ovnqlbidneyujztrswxmckxnap
ovfqlbidfeyujztrswxqckgncp
ovfaybidheyujztrswxrckgnap
ovfqlbidhemujzarnwxmckgnap
ovfqlwidheyujctrsfxmckgnap
ovtelbidheysjztrswxmckgnap
ovfqlbidheyujztrswsmchunap
pvfqlbidheyulztrswxmckynap
ovfqlbzdhezujztfswxmckgnap
ozfqibidheyujztrhwxmckgnap
ovfqlbioheycjztmswxmckgnap
ovfqleidheyujztoswxmckgnhp
ovfqlcidhejujztrswnmckgnap
eqfqlbidheyujztrswxmrkgnap
ovfqlbitheywjztrmwxmckgnap
ovfqlbidheyujptrswnmcggnap
oofqlbidhjyujztrswxmcegnap
ovfqlbidmeyujztrswxmcxgnxp
ovjhlbidhefujztrswxmckgnap
ovfqlbidkeyujzarswxmcugnap
hvyqlridheyujztrswxmckgnap
ovfqlbidheyujxtrswqmckgnpp
ovfqlbidheyuiztrtsxmckgnap
ovfqlbidqeyuuztrbwxmckgnap
ovfqpbidheyujztrswxwekgnap
ovfqltidheyujztrbwxmckgnab
okfxluidheyujztrswxmckgnap
ovfplbidheyujztrsaxmckgnax
wvfqlbidheiujztrswxjckgnap
ovfqlbidheyqjzlrsqxmckgnap
ovfqlbadheyujztrsxxmckgnop
ovfqliidheyujzerswvmckgnap
ovlrlbidheyujztaswxmckgnap
cvzqlbidheyujgtrswxmckqnap
ovfqlbidheyujzuqswxmckgnnp
ovfqlsjdheyujztrswxmcklnap
ovrqlbidheyujztrssrmckgnap
ovcqlbidheyujztrsmxmcognap
ovcqlbidheyjjztrswxmckunap
ovfilbrdhnyujztrswxmckgnap
ovfqlbodheyujztrswxeckqnap
ovfqlbidhuyujqtrswxdckgnap
ovmqlbidheyujderswxmckgnap
ovfylbidheyajzrrswxmckgnap
ovfklbidhtyujzjrswxmckgnap
rvfqlbidheyujztcswxcckgnap
ovfnlyidheyuwztrswxmckgnap
ovfqlbidhexujztrswxmpktnap
ovfplbidheyfjztrswhmckgnap
oyfqlbidmexujztrswxmckgnap
mvfqlbidhcyujztrawxmckgnap
ovfqlbidhqyujdtrswxmcbgnap
ovfqjbidheybjrtrswxmckgnap
ozfqlbidhbyujztrswxmckgpap
okfqlbidheyujztrmwxmckhnap
ovfqlbydheyujzrrswxnckgnap
ovfqtbidheycjztrswxmckgnah
avfqloidheyujztrswxyckgnap
ovfqlbldteyujzyrswxmckgnap
ovfqlbpdhedujztpswxmckgnap
ovfqlbidheyujztrswxucrvnap
ocnqlbidheyujztrswxmwkgnap
ovfqlvidheyujztrswkmckgnlp
evfqlbidheyujzmrswqmckgnap
ovfqlbidhryujztrcwxmekgnap
ovfqlbvdheyujztrzwxmcxgnap
ovfqlridgeyujztrswxmkkgnap
yvfqlbidheyujzthswzmckgnap
ovfqlbidheyujmtrswxyukgnap
ovfqlbidheqgjztrswdmckgnap
dvfzlcidheyujztrswxmckgnap
jvfqlbidheyujztrswxmczgnae
ovfqlbzdheyujztrswxyckgnjp
ovfqlbxdheyujatrswxmcqgnap
ovftlbldheyujztrewxmckgnap
owfqlbitheyujzyrswxmckgnap
ovfqlbidheyujztrswxmchgtvp
ovfqibidheyujzttswxmkkgnap
ovkqlbodheyujztvswxmckgnap
onfqlbbdheyujztrxwxmckgnap
ovfqlbidyeysgztrswxmckgnap
ovfqlbixherujztrswxmcngnap
ovlqlbidfeyujztrswxgckgnap
ovfqlbfdheyujztwswumckgnap
ovfqlbidheeujztrswxmckgkah
ovfqtbqdheyujztrswmmckgnap
bbfqlbigheyujztrswxmckgnap
ovfqljidheyujztrswxmwkgnip
ovfqobidheyujztrsvxmmkgnap
ovfqlbidheydjvtrswxmckanap
ovfqlxidheyujztrswgmckgnep
jvfqlbidhzyujztrswxmckgnay
ovfqlbidhkyujztrswxmlkenap
ovfqobidheyujztrswxmckjnaf
ovfxlbidheyujztrswxmcbgnac
ovfqcbidhtyujztrswxmckqnap
ozfglbidheyujzvrswxmckgnap
ovfqlbidheyujztoswxyckcnap";
    }
}
