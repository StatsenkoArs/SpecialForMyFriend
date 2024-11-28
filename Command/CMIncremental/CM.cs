using System.Collections.Generic;

namespace CMIncremental
{
    public interface ICommand
    {
        void Execute();
    }

    public abstract class ACommand : ICommand
    {
        public void Execute()
        {
            CM.Instance.Registry(this);
            this.doExecute();
        }

        protected abstract void doExecute();
    }

    public delegate void DoExecute();

    public class Command : ACommand
    {
        DoExecute exec;

        public Command(DoExecute exec) => this.exec = exec;

        protected override void doExecute() => exec();
    }

    public class CM
    {
        private CM() { }

        private static CM instance = null;

        public static CM Instance
        {
            get
            {
                if (instance == null) instance = new CM();
                return instance;
            }
        }

        private List<ICommand> items = new List<ICommand>();
        private bool flg_lock = false;

        public void Registry(ICommand c)
        {
            if (flg_lock) return;
            items.Add(c);
        }

        public void Undo()
        {
            if (items.Count <= 1) return;
            flg_lock = true;
            items.RemoveAt(items.Count - 1);
            foreach(var c in items)
            {
                c.Execute();
            }
            flg_lock = false;
        }
    }
}
