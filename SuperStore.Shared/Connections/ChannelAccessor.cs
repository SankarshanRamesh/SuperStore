﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Connections
{
    internal sealed class ChannelAccessor
    {
        private static readonly ThreadLocal<ChannelHolder> Holder = new();

        public IModel? Channel
        {
            get => Holder.Value?.Context;
            set
            {
                var holder = Holder.Value;
                if (holder is not null)
                {
                    holder.Context = null;
                }

                if (value is not null)
                {
                    Holder.Value = new ChannelHolder { Context = value };
                }
            }
        }

        private class ChannelHolder
        {
            public IModel? Context;
        }
    }
}
