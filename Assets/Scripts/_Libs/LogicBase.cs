namespace WonEngine {
    public class LogicBase {
        private int mCheckValue = 0;
        protected const int ERROR = -1;
        protected const int EMPTY = 0;

        protected int CheckValue {
            // Property, write-only
            set { mCheckValue = value; }
        }

        protected bool isSequential (int _data, ref int _length) {
            if (_data == mCheckValue) {
                _length++;
                return true;
            }
            else {
                return false;
            }
        }
    } // end of LogicBase.cs
}