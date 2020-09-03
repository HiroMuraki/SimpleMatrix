using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4 {
    using Element = System.Int32;
    /// <summary>
    /// 简单的行列式类，该行列式被初始化后将无法变更大小
    /// </summary>
    public class Determinant {
        /// <summary>
        /// 内部矩阵
        /// </summary>
        public readonly Element[,] Matrix;
        /// <summary>
        /// 行列式的宽和高
        /// </summary>
        public int Size {
            get {
                return this.Matrix.GetLength(0);
            }
        }
        /// <summary>
        /// 默认构造函数，创建一个1x1的矩阵，用于内部
        /// </summary>
        private Determinant() {
            this.Matrix = new Element[1, 1];
        }
        /// <summary>
        /// 构造函数，传入一个SimpleMatrix
        /// </summary>
        /// <param name="matrix"></param>
        public Determinant(SimpleMatrix matrix) {
            if (matrix.Row != matrix.Column) {
                throw new ArgumentException("行列式的行，列数应相等");
            }
            this.Matrix = new Element[matrix.Row, matrix.Column];
            for (int row = 0; row < matrix.Row; row++) {
                for (int col = 0; col < matrix.Column; col++) {
                    this.Matrix[row, col] = matrix[row, col];
                }
            }
        }
        /// <summary>
        /// 构造函数，传入一个二位数组
        /// </summary>
        /// <param name="matrix">二维数组，其行列应相等</param>
        public Determinant(Element[,] matrix) : this(new SimpleMatrix(matrix)) {

        }
        /// <summary>
        /// 构造函数，矩阵行列数与初始值
        /// </summary>
        /// <param name="size">行列式大小</param>
        /// <param name="initValue">元素初始值</param>
        public Determinant(int size, Element initValue = 0) {
            this.Matrix = new Element[size, size];
            for (int row = 0; row < this.Size; row++) {
                for (int col = 0; col < this.Size; col++) {
                    this.Matrix[row, col] = initValue;
                }
            }
        }
        /// <summary>
        /// 获得代数余子式
        /// </summary>
        /// <param name="rowIndex">划去的行</param>
        /// <param name="columnIndex">划去的列</param>
        /// <returns></returns>
        public Determinant GetAijMatrix(int rowIndex, int columnIndex) {
            int nSize = this.Size - 1;
            Element[,] nMatrix = new Element[nSize, nSize];
            List<Element> elementList = new List<Element>();
            for (int row = 0; row < this.Size; row++) {
                for (int col = 0; col < this.Size; col++) {
                    if (row == rowIndex || col == columnIndex) {
                        continue;
                    }
                    elementList.Add(this.Matrix[row, col]);
                }
            }
            for (int row = 0; row < nSize; row++) {
                for (int col = 0; col < nSize; col++) {
                    nMatrix[row, col] = elementList[row * nSize + col];
                }
            }
            return new Determinant(nMatrix);
        }
        /// <summary>
        /// 获得行列式值
        /// </summary>
        /// <returns>值</returns>
        public Element GetValue() {
            if (this.Size == 2) {
                return this.Matrix[0, 0] * this.Matrix[1, 1] - this.Matrix[0, 1] * this.Matrix[1, 0];
            }
            int sum = 0;
            for (int col = 0; col < this.Size; col++) {
                Element aijMatrixValue = this.Matrix[0, col] * this.GetAijMatrix(0, col).GetValue();
                if (col % 2 == 0) {
                    sum += aijMatrixValue;
                } else {
                    sum -= aijMatrixValue;
                }
            }
            return sum;
        }
        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            StringBuilder matrixString = new StringBuilder();
            for (int row = 0; row < this.Size; row++) {
                for (int col = 0; col < this.Size; col++) {
                    matrixString.Append($"{this.Matrix[row, col]} ");
                }
                matrixString.Append("\n");
            }
            return matrixString.ToString();
        }

    }
}
