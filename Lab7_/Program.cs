using System.Text.Json;
using System.Xml.Serialization;

string path = "..\\temp";

var sq = new Squad() { SquadName = "name", SquadType = "111" };

using (Stream stream = new FileStream($"{path}\\test.json", FileMode.Create, FileAccess.Write)) {
    JsonSerializer.Serialize(stream, sq);
}

Squad parse;
string[] fileList = Directory.GetFiles(path);
int i = 0;
string jsonFile = "";
foreach (var item in fileList) {
    if (item.EndsWith(".json")) {
        jsonFile = item;
        i++;
    }
}
if (i == 1) {
        string json = "";
    using (StreamReader stream = new StreamReader($"{path}\\{jsonFile}")) {
        json = await stream.ReadToEndAsync();
    }



    parse = ParseString(json);



    XmlSerializer xmlser = new XmlSerializer(typeof(Squad));
    using (Stream serialStream = new FileStream($"{path}\\{parse.SquadName} {parse.SquadType}.xml", FileMode.Create)) {
        xmlser.Serialize(serialStream, parse);
    }

}
else { Console.WriteLine("something wrong"); }

static Squad ParseString(string json) {
    Squad parse = new Squad();
    Dictionary<string, string> dict = new Dictionary<string, string>();
    json = json.Replace("\"", "");
    string[] json2 = json.Split(",");
    foreach (var item in json2) {
    string[] json3 = item.Split(":");
        dict.Add(json3[0].Replace("{",""), json3[1].Replace("}", ""));
    }
    foreach (var item in dict) {
        if(item.Key == "SquadName")
            parse.SquadName = item.Value;
        else if(item.Key == "SquadType")
            parse.SquadType = item.Value;
    }

    return parse;
}

[Serializable]
public class Squad {
    public string SquadName { get; set; }
    public string SquadType { get; set; }
}

