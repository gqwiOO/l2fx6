
import socket
import json
import os

if __name__ == '__main__':
    ip = None
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.bind(("192.168.0.101", 6666))
    response = {
                    "id": "1",
                    "time": "9/19/2016 12:00:01 AM",
                    "ip_sender": "192.168.0.100",
                    "ip_receiver": "192.168.0.101",
                    "response": "new one"
                }
    str_response = json.dumps(response)
    # bin_response = ''.join(format(ord(letter), 'b') for letter in str_response)
    bin_response = bytes(str_response, 'utf-8')

    while True:
        data, address = sock.recvfrom(1024)
        if data:
            print("received message %s" % data.decode() + "\n")

            client_response = data.decode()

            json_response = json.loads(client_response)

            if json_response['response'] == "ask to connection":

                print(json_response['ip_sender'] + "want connect to you PC. Y/N?")
                answ = input()

                if answ == "y" or "Y":

                    print("Client asked for connection. Sending DB!...")



                else:
                    #CONNECTION DENIED
                    pass
            with open("func_db.sqlite3", "r") as f:
                bytes_read = f.read().encode()
                sock.sendto(bytes_read, address)
                print(bytes_read)
                print("Sent!")
            # sock.sendto(bin_response, address)


            # elif json_response["response"] == "fhadsiufhdsfjsdkfhsdf":
            #
            #     os.startfile("C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe")


            # Console.WriteLine(Encoding.UTF8.GetString(bytesWithFileFormat).Length + " and " + "SQLite format 3 ".Length);
            # Console.WriteLine(Encoding.ASCII.GetString(bytesWithFileFormat));




