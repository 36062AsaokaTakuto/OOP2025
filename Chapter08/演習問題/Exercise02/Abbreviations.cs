// 略語と対応する日本語を管理するクラス
public class Abbreviations {
    private readonly Dictionary<string, string> _dict = new();

    // コンストラクター
    public Abbreviations() {
        var lines = File.ReadAllLines("./Abbreviations.txt");
        _dict = lines
            .Select(line => line.Split('='))//Split：区切り文字
            .ToDictionary(x => x[0], x => x[1]);
    }

    // 要素を追加
    public void Add(string abbr, string japanese) =>//式形式（=>）
        _dict[abbr] = japanese;

    // 省略語から対応する日本語を取り出す
    public string? Get(string abbr) =>
        _dict.ContainsKey(abbr) ? _dict[abbr] : null;

    // 日本語から対応する省略語を取り出す
    public string? ToAbbreviation(string japanese) =>
        _dict.FirstOrDefault(x => x.Value == japanese).Key;//Valueの中を探すのはFirstOrDefaultを使う

    // 日本語の部分文字列を引数に与え、それが含まれる要素(Key,Value)をすべて取り出す
    public IEnumerable<(string, string)> FindAll(string substring) {
        foreach (var (key, value) in _dict) {
            if (value.Contains(substring)) {
                yield return (key, value);//yield return：要素分returnする
            }
        }
    }

    //8.2.1
    public int Count => _dict.Count;//プロパティgetのみ場合省略可

    //8.2.2
    public bool Remove(string add) => _dict.Remove(add);

    public IEnumerable<KeyValuePair<string,string>> GetAll() {
        return _dict;
    }

}