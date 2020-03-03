using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ICSharpCode.SharpZipLib.Zip;

public class MyZipTest : MonoBehaviour
{

    public Text text;
    public Button btn;
    public M_Progress progress;
    public string zipPath;
    public string unZiPath;
    private void Start()
    {
        //添加回调事件
        MyUnZipCallback._onPreUnzip += OnPreUnzip;
        MyUnZipCallback._onPostUnzip += OnPostUnzip;
        MyUnZipCallback._onFinished += OnFinished;
        btn.onClick.AddListener(StartUnZip);

        //自定义压缩包存放地址与解压地址
        zipPath = @"ZipDirectory/1234.zip";
        Debug.Log(zipPath);
        unZiPath = @"UnZipDirectory";
        Debug.Log(unZiPath);
    }

    //开始解压
    public void StartUnZip()
    {
        ZipUtility.UnzipFile(zipPath, unZiPath, null, new MyUnZipCallback());
    }

    //
    /// <summary>
    /// 当一个文件准备好时调用
    /// </summary>
    /// <param name="_entry"></param>
    /// <returns></returns>
    public bool OnPreUnzip(ZipEntry _entry)
    {
        text.text = "开始解压" + _entry.Name;
        return _entry != null;
    }

    /// <summary>
    /// 当一个文件解压完毕时调用
    /// </summary>
    /// <param name="_entry"></param>
    public void OnPostUnzip(ZipEntry _entry)
    {
        //progress.Value = _entry.
        Debug.Log(_entry.DosTime);
    }

    /// <summary>
    /// 当全部解压完毕时调用
    /// </summary>
    /// <param name="result"></param>
    public void OnFinished(bool result)
    {
        text.text = "解压结束";
        if (result) text.text += "成功";
        else text.text += "失败";
    }
    /// <summary>
    /// 编辑自定义回调类，需继承ZipUtility.UnzipCallback并复写虚函数
    /// </summary>
    public class MyUnZipCallback : ZipUtility.UnzipCallback
    {
        public static Func<ZipEntry,bool> _onPreUnzip;
        public static Action<ZipEntry> _onPostUnzip;
        public static Action<bool> _onFinished;
        public override bool OnPreUnzip(ZipEntry _entry)
        {
            return _onPreUnzip(_entry);
        }

        public override void OnPostUnzip(ZipEntry _entry)
        {
            base.OnPostUnzip(_entry);
            _onPostUnzip(_entry);
        }

        public override void OnFinished(bool _result)
        {
            base.OnFinished(_result);
            _onFinished(_result);
        }
    }
}
