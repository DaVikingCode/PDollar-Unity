using System.IO;
using System.Collections.Generic;
using System.Xml;
using PDollarGestureRecognizer;

using UnityEngine;

namespace PDollarDemo
{
    public class GestureIO
    {
        /// <summary>
        /// Reads a multistroke gesture from an XML file
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
		public static Gesture ReadGesture(string xml)
        {
            List<Point> points = new List<Point>();
            XmlTextReader xmlReader = null;
            int currentStrokeIndex = -1;
            string gestureName = "";
            try
            {
				xmlReader = new XmlTextReader(new StringReader(xml));

                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType != XmlNodeType.Element) continue;
                    switch (xmlReader.Name)
                    {
                        case "Gesture":
                            gestureName = xmlReader["Name"];
                            if (gestureName.Contains("~")) // '~' character is specific to the naming convention of the MMG set
                                gestureName = gestureName.Substring(0, gestureName.LastIndexOf('~'));
                            if (gestureName.Contains("_")) // '_' character is specific to the naming convention of the MMG set
                                gestureName = gestureName.Replace('_', ' ');
                            break;
                        case "Stroke":
                            currentStrokeIndex++;
                            break;
                        case "Point":
                            points.Add(new Point(
                                float.Parse(xmlReader["X"]),
                                float.Parse(xmlReader["Y"]),
                                currentStrokeIndex
                            ));
                            break;
                    }
                }
            }
            finally
            {
                if (xmlReader != null)
                    xmlReader.Close();
            }
            return new Gesture(points.ToArray(), gestureName);
        }

        /// <summary>
        /// Writes a multistroke gesture to an XML file
        /// </summary>
        public static void WriteGesture(PDollarGestureRecognizer.Point[] points, string gestureName, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
                sw.WriteLine("<Gesture Name = \"{0}\">", gestureName);
                int currentStroke = -1;
                for (int i = 0; i < points.Length; i++)
                {
                    if (points[i].StrokeID != currentStroke)
                    {
                        if (i > 0)
                            sw.WriteLine("\t</Stroke>");
                        sw.WriteLine("\t<Stroke>");
                        currentStroke = points[i].StrokeID;
                    }

                    sw.WriteLine("\t\t<Point X = \"{0}\" Y = \"{1}\" T = \"0\" Pressure = \"0\" />",
                        points[i].X, points[i].Y
                    );
                }
                sw.WriteLine("\t</Stroke>");
                sw.WriteLine("</Gesture>");
            }
        }
    }
}