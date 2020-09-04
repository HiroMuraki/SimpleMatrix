using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4 {
    /// <summary>
    /// 用于支持有理数运算
    /// </summary>
    using NumberType = System.Int64;
    public struct RationalNumber {
        /// <summary>
        ///  分子
        /// </summary>
        private NumberType numerator;
        public NumberType Numerator {
            get {
                return this.numerator;
            }
            set {
                this.numerator = value;
            }
        }
        /// <summary>
        /// 分母，不能为0
        /// </summary>
        private NumberType denominator;
        public NumberType Denominator {
            get {
                return this.denominator;
            }
            set {
                if (value == 0) {
                    throw new ArgumentException("分母不能为0");
                }
                this.denominator = value;
            }
        }
        /// <summary>
        /// 获取分数的值，为double类型
        /// </summary>
        public double Value {
            get {
                return (double)this.Numerator / (double)this.Denominator;
            }
        }
        /// <summary>
        /// 构造函数，传入分子和分母
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母，不能为0</param>
        public RationalNumber(NumberType numerator, NumberType denominator) {
            this.numerator = numerator;
            this.denominator = denominator;
        }
        /// <summary>
        /// 构造函数，传入一个用a/b或a\b表示的分数
        /// </summary>
        /// <param name="number">用a/b或a\b形式表示的分数</param>
        public RationalNumber(string number) {
            string[] t = number.Split('\\', '/');
            this.numerator = 1;
            this.denominator = 1;
            try {
                NumberType numerator = NumberType.Parse(t[0]);
                NumberType denominator = 1;
                if (t.Length >= 2) {
                    denominator = NumberType.Parse(t[1]);
                    if (denominator == 0) {
                        throw new ArgumentException("分母不能为0");
                    }
                }
                this.Numerator = numerator;
                this.Denominator = denominator;
            }
            catch (Exception e) {
                throw e;
            }
        }
        /// <summary>
        /// 最简化
        /// </summary>
        /// <returns>分数的最简化形式</returns>
        public RationalNumber Simplify() {
            NumberType gcd = GetGCD(this.Numerator, this.Denominator);
            NumberType numerator = this.Numerator / gcd;
            NumberType denominator = this.Denominator / gcd;
            return new RationalNumber() { Numerator = numerator, Denominator = denominator };
        }
        /// <summary>
        /// 运算:取倒数
        /// </summary>
        /// <returns>分数的倒数</returns>
        public RationalNumber GetReciprocal() {
            return new RationalNumber() { Numerator = this.Denominator, Denominator = this.Numerator };
        }

        #region 运算相关
        /// <summary>
        /// 运算:取反
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static RationalNumber operator -(RationalNumber number) {
            NumberType numerator = -number.Numerator;
            return new RationalNumber(numerator, number.Denominator);
        }
        /// <summary>
        /// 运算:加法
        /// </summary>
        /// <param name="numA"></param>
        /// <param name="numB"></param>
        /// <returns></returns>
        public static RationalNumber operator +(RationalNumber numA, RationalNumber numB) {
            NumberType numerator = numA.Numerator * numB.Denominator + numA.Denominator * numB.Numerator;
            NumberType denominator = numA.Denominator * numB.Denominator;
            return new RationalNumber() { Numerator = numerator, Denominator = denominator }.Simplify();
        }
        /// <summary>
        /// 运算:减法
        /// </summary>
        /// <param name="numA"></param>
        /// <param name="numB"></param>
        /// <returns></returns>
        public static RationalNumber operator -(RationalNumber numA, RationalNumber numB) {
            RationalNumber num = -numB;
            return numA + num;
        }
        /// <summary>
        /// 运算:乘法
        /// </summary>
        /// <param name="numA"></param>
        /// <param name="numB"></param>
        /// <returns></returns>
        public static RationalNumber operator *(RationalNumber numA, RationalNumber numB) {
            NumberType numberator = numA.Numerator * numB.Numerator;
            NumberType denominator = numA.Denominator * numB.Denominator;
            return new RationalNumber() { Numerator = numberator, Denominator = denominator }.Simplify();
        }
        /// <summary>
        /// 运算:除法
        /// </summary>
        /// <param name="numA"></param>
        /// <param name="numB"></param>
        /// <returns></returns>
        public static RationalNumber operator /(RationalNumber numA, RationalNumber numB) {
            RationalNumber num = numB.GetReciprocal();
            return numA * num;
        }
        #endregion

        #region 大小比较
        /// <summary>
        /// 比较:等于
        /// </summary>
        /// <param name="numA"></param>
        /// <param name="numB"></param>
        /// <returns></returns>
        public static bool operator ==(RationalNumber numA, RationalNumber numB) {
            RationalNumber A = numA.Simplify();
            RationalNumber B = numB.Simplify();
            return (A.Numerator == B.Numerator && A.Denominator == B.Denominator);
        }
        /// <summary>
        /// 比较:不等于
        /// </summary>
        /// <param name="numA"></param>
        /// <param name="numB"></param>
        /// <returns></returns>
        public static bool operator !=(RationalNumber numA, RationalNumber numB) {
            return !(numA == numB);
        }
        /// <summary>
        /// 比较:小于
        /// </summary>
        /// <param name="numA"></param>
        /// <param name="numB"></param>
        /// <returns></returns>
        public static bool operator <(RationalNumber numA, RationalNumber numB) {
            NumberType a = numA.Numerator * numB.Denominator;
            NumberType b = numB.Numerator * numA.denominator;
            return a < b;

        }
        /// <summary>
        /// 比较:小于等于
        /// </summary>
        /// <param name="numA"></param>
        /// <param name="numB"></param>
        /// <returns></returns>
        public static bool operator >=(RationalNumber numA, RationalNumber numB) {
            return !(numA < numB);
        }
        /// <summary>
        /// 比较:大于
        /// </summary>
        /// <param name="numA"></param>
        /// <param name="numB"></param>
        /// <returns></returns>
        public static bool operator >(RationalNumber numA, RationalNumber numB) {
            NumberType a = numA.Numerator * numB.Denominator;
            NumberType b = numB.Numerator * numA.Denominator;
            return a > b;
        }
        /// <summary>
        /// 比较:小于等于
        /// </summary>
        /// <param name="numA"></param>
        /// <param name="numB"></param>
        /// <returns></returns>
        public static bool operator <=(RationalNumber numA, RationalNumber numB) {
            return !(numA > numB);
        }
        #endregion

        /// <summary>
        /// 字符串格式化，用a/b表示分数
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            string numString = $"{Math.Abs(this.Numerator)}/{Math.Abs(this.Denominator)}";
            if (this.Denominator * this.Numerator < 0) {
                numString = numString.Insert(0, "-");
            }
            if (Math.Abs(this.Denominator) == 1) {
                numString = numString.Split('/')[0];
            }
            return numString;
        }
        /// <summary>
        /// 获取最大公约数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static NumberType GetGCD(NumberType a, NumberType b) {
            do {
                NumberType t = b;
                b = a % b;
                a = t;
            } while (b != 0);
            return a;
        }
        /// <summary>
        /// 用来消除警告
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
