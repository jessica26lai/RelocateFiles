using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RelocateFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            String src_folder_path = "D:\\xxx\\working\\before";
            String final_folder_path = "D:\\xxx\\working\\after";

            string[] src_folders = Directory.GetDirectories(src_folder_path);

            foreach (string src_path in src_folders)
            {
                String foldername = Path.GetFileNameWithoutExtension(src_path);
                Console.WriteLine(foldername);

                //String src_path = "D:\\xxx";
                String html_path = src_path + "\\html";
                String img_path = src_path + "\\image";

                String html_filename;

                string[] html_files = Directory.GetFiles(html_path);
                //string[] img_files = Directory.GetFiles(img_path);

                foreach (string html_file in html_files)
                {

                    //Console.WriteLine(html_file);
                    html_filename = Path.GetFileNameWithoutExtension(html_file);
                    //Console.WriteLine(html_filename);

                    try
                    {   // Open the text file using a stream reader.
                        using (StreamReader sr = new StreamReader(html_file))
                        {
                            // Read the stream to a string, and write the string to the console.
                            String html_content = sr.ReadToEnd();
                            //Console.WriteLine(html_content);

                            // Here we call Regex.Match for <span class="equipped">560</span>
                            Match match = Regex.Match(html_content, @"<img src=\""\.\./image/(.+\.\w\wg)\""", RegexOptions.IgnoreCase);

                            // Here we check the Match instance.
                            if (match.Success)
                            {
                                string key = match.Groups[1].Value; //result here
                                //System.IO.File.Move(img_path + "\\" + key, img_path + "\\" + html_filename + ".jpg");
                                System.IO.Directory.CreateDirectory(final_folder_path + "\\" + foldername);
                                if (html_filename == "cover.jpg"){
                                    System.IO.File.Copy(img_path + "\\" + key, final_folder_path + "\\" + foldername + "\\" + key);
                                }
                                else {
                                    System.IO.File.Copy(img_path + "\\" + key, final_folder_path + "\\" + foldername + "\\" + html_filename + ".jpg");
                                }
                            }

                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("The file could not be read:");
                        Console.WriteLine(e.Message);
                    }



                }

            }

            //foreach (string img_file in img_files)
            //{
            //    Console.WriteLine(img_file);
            //}
            Console.WriteLine("Program End");
            Console.ReadKey();
        }

    }
}
