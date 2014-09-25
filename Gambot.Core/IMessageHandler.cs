﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gambot.Core
{
    public interface IMessageHandler
    {
        void Initialize(IDataStore dataStore);
        bool Digest(IMessenger messenger, IMessage message, bool addressed);
    }
}
