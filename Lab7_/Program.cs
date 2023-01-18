using System.Text.Json;
using System.Xml.Serialization;

string path = "..\\temp";

var sq = new Squad() { squadName = "name", squadType = "111" };

//using (Stream stream = new FileStream($"{path}\\test.json", FileMode.Create, FileAccess.Write)) {
//    JsonSerializer.Serialize(stream, sq);
//}

Squad parse;
string[] fileList = Directory.GetFiles(path);
int i = 0;
string jsonFile = "";
foreach (var item in fileList) {
    if (item.Contains(".json")) {
        jsonFile = item;
        i++;
    }
}
if (i == 1) {
    //fileList.SingleOrDefault(".json");
    using (Stream stream = new FileStream($"{path}\\{jsonFile}", FileMode.Open, FileAccess.Read)) {
        parse = JsonSerializer.Deserialize<Squad>(stream);
    }

    XmlSerializer xmlser = new XmlSerializer(typeof(Squad));

    using (Stream serialStream = new FileStream($"{path}\\{parse.squadName} {parse.squadType}.xml", FileMode.Create)) {
        xmlser.Serialize(serialStream, parse);
    }

    Console.WriteLine(parse.squadName);
}
else { Console.WriteLine("something wrong"); }


[Serializable]
public class Squad {
    public string squadName { get; set; }
    public string squadType { get; set; }

}