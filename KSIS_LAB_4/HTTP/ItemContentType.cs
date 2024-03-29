﻿using System;
using System.Text.RegularExpressions;

namespace KSIS_LAB_4.HTTP
{
    public class ItemContentType : ItemBase
  {

    private string _Value = "text/plain";
    private string _Charset = "utf8";

    /// <summary>
    /// Тип содержимого
    /// </summary>
    public string Value
    {
      get
      {
        return _Value;
      }
    }

    /// <summary>
    /// Кодировка
    /// </summary>
    public string Charset
    {
      get
      {
        return _Charset;
      }
    }

    public ItemContentType(string source) : base(source)
    {
      if (String.IsNullOrEmpty(source)) return;
      // ищем в источнике первое вхождение точки с запятой
      int typeTail = source.IndexOf(";");
      if (typeTail == -1)
      { // все содержимое источника является информацией о типа
        _Value = source.Trim().ToLower();
        return; // других параметров нет, выходим
      }
      _Value = source.Substring(0, typeTail).Trim().ToLower();
      // парсим параметры
      string p = source.Substring(typeTail + 1, source.Length - typeTail - 1);
      Regex myReg = new Regex(@"(?<key>.+?)=((""(?<value>.+?)"")|((?<value>[^\;]+)))[\;]{0,1}", RegexOptions.Singleline);
      MatchCollection mc = myReg.Matches(p);
      foreach (Match m in mc)
      {
        if (m.Groups["key"].Value.Trim().ToLower() == "charset")
        {
          _Charset = m.Groups["value"].Value;
          // можно добавить обработку и других параметров, если таковые будут, что маловероятно
        }
      }
    }

  }
}
