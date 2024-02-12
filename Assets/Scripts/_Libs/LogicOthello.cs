namespace WonEngine {
    public class LogicOthello : Logic2D {
        public LogicOthello (int r, int c) : base (r, c) {
            // constructor
            setValue (1, 3, 4);
            setValue (1, 4, 3);
            setValue (2, 3, 3);
            setValue (2, 4, 4);
        }

        public bool setData (int r, int c) {
            if (!isEmpty (r, c)) return false;
            if (!analyze (r, c)) return false; // 분석 결과 false 이면 바뀌는 돌이 없는 상황

            setValue (mTurn, r, c);
            nextTurn ();
            return true;
        }

        public override bool analyze (int r, int c) {
            int checkvalue = 3 - mTurn;
            resetLength ();

            for (Direction dir = Direction.U; dir <= Direction.UL; dir++) {
                int beforeLength = mLength;
                if (analyzeDirection (checkvalue, (int)dir, r, c)) {
                    mLength = beforeLength;
                }
            }

            if (mLength > 0) {
                for (int i = 0; i < mLength; i++) {
                    setValue (mTurn, mPoints[i].r, mPoints[i].c);
                }

                return true;
            }

            return false;
        }
    } // end of LogicOthello.cs
}