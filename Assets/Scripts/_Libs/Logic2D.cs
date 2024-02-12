namespace WonEngine {
    public abstract class Logic2D : LogicBase {
        private int[,] mDat; // 2차원 데이터를 보관할 배열 변수
        private int mRow, mCol; // Dataset의 행(row), 열(column)의 값
        private const int DATASIZE = 100; // 위치 정보를 보관해야 할 Point들의 최대 개수

        protected int mTurn; // 턴 정보 (1-2-1-2-1-2-1-2 형태로 흘러간다)
        protected int mLength; // 체크해야 할 수가 수열에서 연속적으로 연결된 길이값

        // Property
        public int Length {
            // read-only
            get { return mLength; }
        }

        public int Turn {
            // read-only
            get { return mTurn; }
        }

        protected class Point {
            // inner class
            public int r, c;
        }

        protected Point[] mPoints; // 기억해야 할 위치 정보 배열 변수

        // 자식 클래스에서 반드시 구현해야 하는 추상 함수
        public abstract bool analyze (int r, int c);

        protected enum Direction {
            U, // up
            UR, // up-right
            R, // right
            DR, // down-right
            D, // down
            DL, // down-left
            L, // left
            UL // up-left
        }

        protected int[,] mCursorMove = new int[,] {
            { -1, 0 }, // up
            { -1, 1 }, // up-right
            { 0, 1 }, // right
            { 1, 1 }, // down-right
            { 1, 0 }, // down
            { 1, -1 }, // down-left
            { 0, -1 }, // left
            { -1, -1 } // up-left
        };

        public Logic2D (int _r, int _c) {
            // constructor
            mRow = _r;
            mCol = _c;
            initData ();
        }

        protected void initData () {
            mDat = new int[mRow, mCol];
            mLength = 0;
            mTurn = 1;
            mPoints = new Point [DATASIZE];
            for (int i = 0; i < mPoints.Length; i++) mPoints[i] = new Point ();
        }

        protected bool analyzeDirection (int cv, int dir, int sr, int sc) {
            CheckValue = cv;
            for (int r = sr, c = sc;
                 (0 <= r && r < mRow) && (0 <= c && c < mCol);
                 r += mCursorMove[dir, 0], c += mCursorMove[dir, 1]) {
                // (sr, sc)에서 dir 방향으로 끝까지 체크할 값으로 탐색을 진행한다.
                if (r == sr && c == sc) continue;

                if (!isSequential (mDat[r, c], ref mLength)) {
                    // 체크할 값의 연속성이 깨짐.
                    return mDat[r, c] == EMPTY;
                }
                else {
                    // 체크할 값의 연속성이 유지되고 있음. 연속성이 유지된 지점들을 저장해 둔다.
                    int index = mLength - 1;
                    mPoints[index].r = r;
                    mPoints[index].c = c;
                }
            }

            return true;
        }

        protected bool isEmpty (int r, int c) {
            return mDat[r, c] == EMPTY;
        }

        public void nextTurn () {
            mTurn = 3 - mTurn;
        }

        protected void resetLength () {
            mLength = 0;
        }

        protected void setValue (int value, int r, int c) {
            mDat[r, c] = value;
        }

        public int getValue (int r, int c) {
            return mDat[r, c];
        }

        public int[,] getData () {
            return mDat;
        }

        public int getPoints (int pos, int index) {
            if (pos == 0) return mPoints[index].r;
            else return mPoints[index].c;
        }
    } // end of Logic2D.cs
}