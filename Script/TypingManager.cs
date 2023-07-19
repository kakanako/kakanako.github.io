using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;//TextMeshPro

public class TypingManager : MonoBehaviour
{
    // （問題）日本語文
    private string[] qJ = {"プログラミング","タイピング","パンダ","牛","命","一期一会","猿も木から落ちる","ff16が発売されましたね","茨城県はいばらきけん","つくば市","理想科学工業株式会社"};
    // （問題）ローマ字文
    private string[] qR = {"puroguraminngu","taipinngu","pannda","usi","inoti","itigoitie","sarumokikaraotiru","ff16gahatubaisaremasitane","ibarakikennhaibarakikenn","tukubasi","risoukagakukougyoukabusikigaisya"};
    // （表示）日本語
    private TextMeshProUGUI UIJ;
    // （表示）ローマ字
    private TextMeshProUGUI UIR;
    // （問題）日本語
    private string nQJ;
    // （問題）ローマ字
    private string nQR;
    // 問題番号
    private int numberOfQuestion;

    // 問題の何文字目かを保存する変数
    private int indexOfString;

    // 入力した文字列テキスト
    private TextMeshProUGUI UII;
    // 正解数
    private int correctN;
    // 正解数表示用テキストUI
    private TextMeshProUGUI UIcorrectA;
    // 正解した文字列を入れておくもの
    private string correctString;

    // 間違った数
    private int mistakeN;
    // 間違った数表示用テキストUI
    private TextMeshProUGUI UImistake;

    // 正解率
    private float correctAR;
    // 正解率表示用テキストUI
    private TextMeshProUGUI UIcorrectAR;

    // Windows or Mac
    private bool _isWindows;
    private bool _isMac;
    private bool _isOthers;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("------RISO TYPING GAME START------");
        //端末の種類を取得
        if (SystemInfo.operatingSystem.Contains("Windows"))
        {
            _isWindows = true;
            _isMac = false;
            _isOthers = false;
        }
        else if (SystemInfo.operatingSystem.Contains("Mac"))
        {
            _isWindows = false;
            _isMac = true;
            _isOthers = false;
        }
        else
        {
            _isWindows = false;
            _isMac = false;
            _isOthers = true;
        }

        //テキストUIを取得
        UIJ = transform.Find("QuestionJ").GetComponent<TextMeshProUGUI>();
        UIR = transform.Find("QuestionR").GetComponent<TextMeshProUGUI>();
        UII = transform.Find("Input").GetComponent<TextMeshProUGUI>();
        UIcorrectA = transform.Find("CorrectCount").GetComponent<TextMeshProUGUI>();
        UImistake = transform.Find("MisstakeCount").GetComponent<TextMeshProUGUI>();
        UIcorrectAR = transform.Find("CorrectRate").GetComponent<TextMeshProUGUI>();

        // 初期化
        correctN = 0;//正解数
        UIcorrectA.text = "正解数：" + correctN.ToString();
        mistakeN = 0;//間違った数
        UImistake.text = "間違った数：" + mistakeN.ToString();
        correctAR = 0;//正解率
        UIcorrectAR.text = "正解率：" + correctAR.ToString();

        // 新しい問題を表示するメソッドを呼び出す
        QutputQuestion();
        
    }

    // Update is called once per frame
    void Update()
    {
        // 今見ている文字とキーボードから打った文字が同じかどうか　Input.inputStringに入力した文字が入らない。-> OnGUI()メソッドで実施する形に変更した。
        /*
        if (Input.GetKeyDown(nQR[indexOfString].ToString()))
        {
            Debug.Log("成功した文字：" + Input.inputString);
            Correct();// 正解
            if(indexOfString >= nQR.Length)//１文が終わったら次の問題へ
            {
                QutputQuestion();
            }
        }
        else if(Input.anyKeyDown){// 失敗
            Debug.Log("失敗した文字：" + Input.inputString);
            Mistake();
        }
        else
        {
            ;
        }*/

    }

    // 何かしらの入力イベントが生じた時に呼ばれるイベント関数
    private void OnGUI()
    {
        if(Event.current.type == EventType.KeyDown)
        {
            switch (InputKey(GetStringFromKeyCode(Event.current.keyCode)))
            {
                case 1:
                    Correct();// 正解
                    if (indexOfString >= nQR.Length)//１文が終わったら次の問題へ
                    {
                        QutputQuestion();
                    }
                    break;
                case 2:
                    Mistake();//失敗
                    break;
            }
        }
    }

    // タイピングの正誤判定
    int InputKey(string inputChar){
        if (inputChar == "\0")
            {
                return 0;// 何もしない
            }
            if (inputChar == nQR[indexOfString].ToString())
            {
                return 1;// 正解時
            }
            return 2;// 間違った時
    }

    // 新しい問題を表示するメソッド
    void QutputQuestion()
    {
        // テキストUIの初期化
        UIJ.text = "";
        UIR.text = "";
        UII.text = "";
        correctString = "";
        // 文字の位置を0番目に戻す
        indexOfString = 0;

        //ランダムに問題を選ぶ
        numberOfQuestion = Random.Range(0, qJ.Length);

        //選んだ問題をテキストUIにセットする
        nQJ = qJ[numberOfQuestion];
        nQR = qR[numberOfQuestion];
        UIJ.text = nQJ;
        UIR.text = nQR;
    }

    // タイピング正解時の処理
    void Correct()
    {
        Debug.Log("正解の処理");

        // 正解数をカウント
        correctN++;
        UIcorrectA.text = "正解文字数：" + correctN.ToString();
        // 正解率の計算
        CorrectAnswerRate();
        // 正解した文字を表示
        correctString += nQR[indexOfString].ToString();
        UII.text = correctString;
        // 次の文字へ
        indexOfString++;
    }

    // タイピング失敗時の処理
    void Mistake()
    {
        string MistakeKey = GetStringFromKeyCode(Event.current.keyCode);
        char MistakeKeyChar = GetCharFromKeyCode(Event.current.keyCode);

        //Debug.Log("失敗の処理");
        //mistakeN += MistakeKey.Length; //同時押しに対応

        // タイピングミス数のカウント
        if (MistakeKey == null)
        {
            ;//NULLなので何もしない Debug.Log("NULLなので何もしない");
        }
        else
        {
            Debug.Log("失敗：タイピングミス数をカウントする");
            mistakeN += MistakeKey.Length; //同時押しに対応
        }
        // タイピングミスのテキスト表示更新
        UImistake.text = "不正解文字数：" + mistakeN.ToString();

        // 正解率の計算
        CorrectAnswerRate();

        //間違って入力した文字を赤文字で表示する。
        if(MistakeKeyChar == '\0')
        {
            ;// nullは何もしない
        }
        else
        {
            Debug.Log("間違って入力した文字は" + MistakeKeyChar);
            UII.text = correctString + "<color=#ff0000ff>" + MistakeKeyChar + "</color>";
        }

        /* //string版 WebGLにビルドすると、赤字にならないw
        // 間違った数をカウント（同時押しも対応させる）
        //MistakeKeyにはNULLが入ってくる場合があるので条件を分ける。
        if (MistakeKey == null)
        {
            //mistakeN++;//素直にプラスする
            Debug.Log("NULLなので何もしない");
        }
        else
        {
            Debug.Log("失敗の処理");
            mistakeN += MistakeKey.Length; //同時押しに対応
        }
        UImistake.text = "不正解文字数：" + mistakeN.ToString();
        // 正解率の計算
        CorrectAnswerRate();

        if (MistakeKey != "")
        {
            Debug.Log("間違って入力した文字は" + MistakeKey);
            UII.text = correctString + "<color=#ff0000ff>" + MistakeKey + "</color>";
        }

        */

        // 失敗した文字を表示する(赤文字で。)
        //if (Input.inputString != "")
        //(checkString != null) && (checkString.Length != 0)
        //if (MistakeKey != "")
        //{
        //    Debug.Log("間違って入力した文字は" + MistakeKey);
        //    //UII.text = correctString + "<color=#ff0000ff>" + Input.inputString + "</color>";
        //    UII.text = correctString + "<color=#ff0000ff>" + MistakeKey + "</color>";
        //}
    }

    // 正解率の計算処理
    void CorrectAnswerRate()
    {
        Debug.Log("正解率の計算の処理");

        // 正解率の計算
        correctAR = 100f * correctN / (correctN + mistakeN);
        // 小数点以下２位まで表示する
        UIcorrectAR.text = "正解率：" + correctAR.ToString("0.00");
    }

    // KeyCodeを文字列stringに変換する関数
    string GetStringFromKeyCode(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.A:
                return "a";
            case KeyCode.B:
                return "b";
            case KeyCode.C:
                return "c";
            case KeyCode.D:
                return "d";
            case KeyCode.E:
                return "e";
            case KeyCode.F:
                return "f";
            case KeyCode.G:
                return "g";
            case KeyCode.H:
                return "h";
            case KeyCode.I:
                return "i";
            case KeyCode.J:
                return "j";
            case KeyCode.K:
                return "k";
            case KeyCode.L:
                return "l";
            case KeyCode.M:
                return "m";
            case KeyCode.N:
                return "n";
            case KeyCode.O:
                return "o";
            case KeyCode.P:
                return "p";
            case KeyCode.Q:
                return "q";
            case KeyCode.R:
                return "r";
            case KeyCode.S:
                return "s";
            case KeyCode.T:
                return "t";
            case KeyCode.U:
                return "u";
            case KeyCode.V:
                return "v";
            case KeyCode.W:
                return "w";
            case KeyCode.X:
                return "x";
            case KeyCode.Y:
                return "y";
            case KeyCode.Z:
                return "z";
            case KeyCode.Alpha0:
                return "0";
            case KeyCode.Alpha1:
                return "1";
            case KeyCode.Alpha2:
                return "2";
            case KeyCode.Alpha3:
                return "3";
            case KeyCode.Alpha4:
                return "4";
            case KeyCode.Alpha5:
                return "5";
            case KeyCode.Alpha6:
                return "6";
            case KeyCode.Alpha7:
                return "7";
            case KeyCode.Alpha8:
                return "8";
            case KeyCode.Alpha9:
                return "9";
            case KeyCode.Minus:
                return "-";
            case KeyCode.Caret:
                return "^";
            case KeyCode.Backslash:
                return "\\";
            case KeyCode.At:
                return "@";
            case KeyCode.LeftBracket:
                return "[";
            case KeyCode.Semicolon:
                return ";";
            case KeyCode.Colon:
                return ":";
            case KeyCode.RightBracket:
                return "]";
            case KeyCode.Comma:
                return ",";
            case KeyCode.Period:
                return ".";
            case KeyCode.Slash:
                return "/";
            case KeyCode.Underscore:
                return "_";
            case KeyCode.Backspace:
                return "\b";
            case KeyCode.Return:
                return "\r";
            case KeyCode.Space:
                return " ";
            default: //上記以外のキーが押された場合は「null文字」を返す。
                return null;
        }
    }

    // KeyCodeを文字列charに変換する関数
    char GetCharFromKeyCode(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.A:
                return 'a';
            case KeyCode.B:
                return 'b';
            case KeyCode.C:
                return 'c';
            case KeyCode.D:
                return 'd';
            case KeyCode.E:
                return 'e';
            case KeyCode.F:
                return 'f';
            case KeyCode.G:
                return 'g';
            case KeyCode.H:
                return 'h';
            case KeyCode.I:
                return 'i';
            case KeyCode.J:
                return 'j';
            case KeyCode.K:
                return 'k';
            case KeyCode.L:
                return 'l';
            case KeyCode.M:
                return 'm';
            case KeyCode.N:
                return 'n';
            case KeyCode.O:
                return 'o';
            case KeyCode.P:
                return 'p';
            case KeyCode.Q:
                return 'q';
            case KeyCode.R:
                return 'r';
            case KeyCode.S:
                return 's';
            case KeyCode.T:
                return 't';
            case KeyCode.U:
                return 'u';
            case KeyCode.V:
                return 'v';
            case KeyCode.W:
                return 'w';
            case KeyCode.X:
                return 'x';
            case KeyCode.Y:
                return 'y';
            case KeyCode.Z:
                return 'z';
            case KeyCode.Alpha0:
                return '0';
            case KeyCode.Alpha1:
                return '1';
            case KeyCode.Alpha2:
                return '2';
            case KeyCode.Alpha3:
                return '3';
            case KeyCode.Alpha4:
                return '4';
            case KeyCode.Alpha5:
                return '5';
            case KeyCode.Alpha6:
                return '6';
            case KeyCode.Alpha7:
                return '7';
            case KeyCode.Alpha8:
                return '8';
            case KeyCode.Alpha9:
                return '9';
            case KeyCode.Minus:
                return '-';
            case KeyCode.Caret:
                return '^';
            case KeyCode.Backslash:
                return '\\';
            case KeyCode.At:
                return '@';
            case KeyCode.LeftBracket:
                return '[';
            case KeyCode.Semicolon:
                return ';';
            case KeyCode.Colon:
                return ':';
            case KeyCode.RightBracket:
                return ']';
            case KeyCode.Comma:
                return ',';
            case KeyCode.Period:
                return '.';
            case KeyCode.Slash:
                return '/';
            case KeyCode.Underscore:
                return '_';
            case KeyCode.Backspace:
                return '\b';
            case KeyCode.Return:
                return '\r';
            case KeyCode.Space:
                return ' ';
            default: //上記以外のキーが押された場合は「null文字」を返す。
                return '\0';
        }
    }

}
