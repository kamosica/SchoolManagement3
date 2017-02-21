using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;//リストに必要

public class CsvManager : MonoBehaviour {

    //string[,] _box;
    public int LengthX;
    public int LengthY;


    void Start () {

        //CsvRead("sample");
        //CsvWrite("test");

    }

	void Update () {
	
	}

    //CSVファイルの読み込み関数
    public string[,] CsvRead(string filename)
    {
        //テキストファイルの読み込み
        TextAsset _fieldTxt = Resources.Load("CSV/" + filename, typeof(TextAsset)) as TextAsset;
        //区切り条件指定
        //char[] _kugiri = {'\r','\n'};//改行区切り、二種類
        char[] _kugiri = { '\n' };
        //String配列に格納
        string[] _fieldString = _fieldTxt.text.Split(_kugiri);  //行を配列に格納
        string[] _sizeX = _fieldString[0].Split(',');

        LengthY = _fieldString.Length - 1;  //Y方向の配列の長さ
        LengthX = _sizeX.Length;            //X方向の配列の長さ

        string[,] _box = new string[LengthY, LengthX];

        for (int i = 0; i < _fieldString.Length - 1; i++)
        {
            string[] _tempLetter = _fieldString[i].Split(','); //行をカンマで分解して配列に格納

            for (int j = 0; j < _tempLetter.Length; j++)
            {
                _box[i, j] = _tempLetter[j];
                //Debug.Log(i + ":" + j + "   " + _box[i, j]);
            }
        }

        return _box;

        //Array_Log(_box);
    }

    //CSVファイルの書き込み関数
    public void CsvWrite(string filename,int[,] array)
    {
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo("Assets/Resources/CSV/" + filename + ".csv");
        //sw = fi.AppendText(); //追加書き込み
        sw = fi.CreateText();   //新規書き込み

        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if(j == array.GetLength(1) - 1) sw.Write(array[i, j] + "\n");
                else sw.Write(array[i,j] + ",");
            }
        }

        sw.Flush();
        sw.Close();
    }

    public void CsvWrite(string filename, List<string[]> array)
    {
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo("Assets/Resources/CSV/" + filename + ".csv");
        //sw = fi.AppendText(); //追加書き込み
        sw = fi.CreateText();   //新規書き込み

        for (int i = 0; i < array.Count; i++)
        {
            string[] str = array[i];

            for (int j = 0; j < str.Length ; j++)
            {
                if (j == str.Length - 1) sw.Write(str[j] + "\n");
                else sw.Write(str[j] + ",");
            }
        }

        sw.Flush();
        sw.Close();
    }

    //2次元配列をデバックログで表示する関数
    void Array_Log(string[,] array)
    {
        string print_array = "";
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                print_array += array[i, j].ToString() + ":";
            }
            print_array += "\n";
        }

        Debug.Log(print_array);
    }
}
