using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;

namespace FileReaderLibrary
{
    public class ReadFile2
    {

        FileStream fs;
        string line = "";
        StreamWriter sw;
        string[] archt = new string[] { "Network", "Memory", "Hardware" };




        public int GetTotalWeeks()
        {
            string FileToRead = "infralog.txt";
            string[] words;
            List<int> weeks = new List<int>();
            List<int> weeknodpulicate = new List<int>();

            using (StreamReader reader = new StreamReader(FileToRead))
            {
                string line;
                //reader object reads a single line at a time 
                //stores it in the variable string line
                while ((line = reader.ReadLine()) != null)
                {

                    words = line.Split(',');
                    int r = Convert.ToInt32(words[4]);
                    weeks.Add(r);

                }

                weeknodpulicate = weeks.Distinct().ToList();
            }
            return weeknodpulicate.Last();

        }

        public String Weekwisedata(int week)
        {
            string result = "";
            string FileToRead = "infralog.txt";
            string[] words;

            using (StreamReader reader = new StreamReader(FileToRead))
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                string line;
                for (int i = 0; i < 3; i++)
                {
                    int breach = 0;
                    int nobreach = 0;

                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    while ((line = reader.ReadLine()) != null)
                    {
                        words = line.Split(',');
                        if (Convert.ToInt32(words[4]) == week)
                        {


                            if (words[1].Equals(archt[i]))
                            {

                                if (Convert.ToInt32(words[2]) > 100)
                                {
                                    breach++;
                                }
                                else
                                {
                                    nobreach++;
                                }
                            }
                        }

                    }
                    result = result + $"\n Week {week}: {archt[i]} [ Breach = {breach} || No breach = {nobreach}] ";
                }
            }

            return result;
        }

        public void write_logfile(string res)
        {
            FileStream fs = new FileStream("logreport.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(res);

            sw.Flush();
            sw.Close();
            fs.Close();
        }

    }
}
