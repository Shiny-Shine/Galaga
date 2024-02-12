namespace WonEngine {
    public class LogicOmok : Logic2D {
        public LogicOmok (int r, int c) : base (r, c) {
            // constructor
        }

        public bool setData (int r, int c) {
            if (!isEmpty (r, c)) return false;

            bool res = analyze (r, c);
            setValue (mTurn, r, c);
            nextTurn ();

            return res;
        }

        public override bool analyze (int r, int c) {
            int checkvalue = mTurn;

            for (Direction dir = Direction.U; dir <= Direction.DR; dir++) {
                analyzeDirection (checkvalue, (int)dir, r, c);
                analyzeDirection (checkvalue, (int)dir + 4, r, c); // 맞은 편 방향성 탐색

                if (mLength >= 4) return true;
                resetLength ();
            }

            return false;
        }
    } // end of LogicOmok.cs
}