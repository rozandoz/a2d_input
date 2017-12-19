using System;

namespace a2d_input.Commands
{
    public abstract class CommandHandler<T> : IHandler where T : CommandOptions
    {
        protected CommandHandler(T options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            Options = options;
        }

        protected T Options { get; }

        public int Run()
        {
            return OnRun(Options);
        }

        protected abstract int OnRun(T options);
    }
}