using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Xamarin.Essentials;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Data.Sqlite;

namespace AndroiApp.Classes
{
    internal class Server
    {
        UdpClient udpClient = new UdpClient();

        /// <summary>
        /// It's a bad idea, after checking response program should every time gives this var value null
        /// </summary>

        public byte[] lastResponse = null;
        private const string databaseFilePath = "dataBase.sqlite3";
        public Dictionary<string, string> functionsFromDatabase = new Dictionary<string, string>();

        public async void sendResponse(string response, string IP)
        {
            /// <summary>
            /// Takes string with description of response and receiver's IP, puts it in JSON format
            /// and converts it into bytes, then sends it on receiver's IP
            /// </summary>
            Dictionary<string, string> jsonDictionaryResponse = new Dictionary<string, string>
            {
                { "time", "9/19/2016 12:00:00 AM"},
                { "ip_sender", "192.168.0.100"},
                {"ip_receiver", "192.168.0.101" },
                { "response", $"{response}" }
            };
            Console.WriteLine(jsonDictionaryResponse["response"] + " response");
            jsonDictionaryResponse["ip_sender"] = "a";
            //jsonDictionaryResponse["response"] = response;
            jsonDictionaryResponse["ip_receiver"] = IP;

            string JSONDictionaryInString = JsonConvert.SerializeObject(jsonDictionaryResponse);

            byte[] dictionaryInBytes = Encoding.UTF8.GetBytes(JSONDictionaryInString);

            await udpClient.SendAsync(dictionaryInBytes, dictionaryInBytes.Length, new IPEndPoint(IPAddress.Parse(IP), 6666));

        }

        public static Dictionary<string, string> getJsonDictionaryFromBytes(byte[] bytes)
        {
            /// 
            /// Converts bytes into dictionary. Maybe need to add try block.
            /// 

            string JSONDictionaryInString = Encoding.UTF8.GetString(bytes);
            Dictionary<string, string> JSONDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JSONDictionaryInString);

            return JSONDictionary;
        }

        public static bool isResponseSQLiteFormat(byte[] bytes)
        {
            ///
            /// Takes first 16 bytes of array and convert it into string. This bytes contains file format
            /// if it's sqlite3.
            ///
            byte[] bytesWithFileFormat = new byte[16];
            for (int index = 0; index < 16; index++)
            {
                bytesWithFileFormat[index] = bytes[index];
            }
            /// Does not return true if string equals value here |
            ///                                                  v
            return Encoding.UTF8.GetString(bytesWithFileFormat) == "SQLite format 3 ";
        }
        public static bool isResponseJSONFormat(byte[] bytes)
        {
            string strInput = Encoding.UTF8.GetString(bytes);

            if (string.IsNullOrWhiteSpace(strInput))
            {
                return false;
            }

            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void SaveSQLiteDatabaseFromBytesArray(byte[] data)
        {
            ///
            /// Reads response bytes and saves it into sqlite3 format in project dir.
            ///
            Console.WriteLine("Database saved!");

            var writer = new BinaryWriter(File.OpenWrite(databaseFilePath));
            writer.Write(data);
        }

        public void GetDictionaryFunctionsFromDatabase()
        {
            const string selectNameAndSecretCodeFromFunctionsTable = "SELECT NAME, secret_message FROM functions";


            using (var connection = new SqliteConnection($"Data source={databaseFilePath}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(selectNameAndSecretCodeFromFunctionsTable, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var name = reader.GetValue(0);
                            var secret_message = reader.GetValue(1);

                            Console.WriteLine($"name of func : {name}, code : {secret_message}");
                            functionsFromDatabase[$"{name}"] = $"{secret_message}";
                        }
                    }
                }
            }
        }


        public async void receiveResponse()
        /// <summary>
        /// Does not work well, takes all socket's memory after no responses
        /// </summary>
        {
            try
            {
                UdpReceiveResult response = await udpClient.ReceiveAsync();
                if (response != null)
                {
                    lastResponse = response.Buffer;
                }
            }
            catch (Exception e)
            {
                /// Need add other exceptions.
                Console.WriteLine("XD");
            }
        }
    }
}
