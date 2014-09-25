﻿using System;
using System.Threading;
using Gambot.Core;

namespace Gambot.IO.Console
{
    public class ConsoleMessenger : IMessenger
    {
        protected Thread inputThread;
        protected string name;

        public event EventHandler<MessageEventArgs> MessageReceived;

        public ConsoleMessenger()
        {
            inputThread = new Thread(() =>
            {
                while (true)
                {
                    System.Console.Write("{0}:\t", Environment.UserName);
                    var message = System.Console.ReadLine();
                    if (MessageReceived != null)
                    {
                        MessageReceived(this,
                            new MessageEventArgs
                            {
                                Message = new ConsoleMessage(message),
                                Addressed = true
                            });
                    }
                }
            });
            inputThread.Start();
            name = Config.Get("Name", "gambot");
        }

        public void SendMessage(string message, string destination)
        {
            System.Console.WriteLine("{0}:\t{1}", name, message);
        }

        public void Dispose()
        {
            inputThread.Abort();
        }
    }
}