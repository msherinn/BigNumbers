using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1;

public class BigInteger
{
    private int[] _numbers;
    private bool isNegative = false;

    public BigInteger(string value)
    {
        if (value[0] == '-')
        {
            isNegative = true;
            value = value.Substring(1);
        }

        _numbers = new int[value.Length];
        
        var j = 0;
        for (var i = value.Length - 1; i >= 0; i--)
        {
            _numbers[j] = (int) Char.GetNumericValue(value[i]);
            j++;
        }
    }

    public int[] numbers => _numbers;
    public static BigInteger operator +(BigInteger a, BigInteger b) => a.Add(b);
    public static BigInteger operator -(BigInteger a, BigInteger b) => a.Sub(b);
    public static BigInteger operator *(BigInteger a, BigInteger b) => a.Multiply(b);

    public static BigInteger operator ++(BigInteger a) => a + new BigInteger("1");
    
    public bool IsNegative
    {
        get { return isNegative; }
        set { isNegative = value; }
    }

    public int Length()
    {
        return _numbers.Length;
    }
    
    public override string ToString()
    {
        var strNumber = "";
        
        if (isNegative == true)
        {
            strNumber += '-';
        }
        
        for (var i = _numbers.Length - 1; i >=0; i--)
        {
            strNumber += Convert.ToString(_numbers[i]);
        }

        return strNumber;
    }

    private BigInteger Sum(int[] a, int[] b)
    {
        var smaller = a.Length <= b.Length ? a : b;
        var bigger = a.Length > b.Length ? a : b;
        var resultNumbers = new int[bigger.Length + 1];
        var remainder = 0;
        var strNumber = "";
        
        for (var i = 0; i < smaller.Length; i++)
        {
            var result = new int();
            result = bigger[i] + smaller[i] + remainder;
            
            if (result >= 10)
            {
                resultNumbers[i] = result - 10;
                remainder = 1;
            }

            else
            {
                resultNumbers[i] = result;
                remainder = 0;
            }
        }

        if (remainder != 0)
        {
            if (smaller.Length == bigger.Length)
            {
                resultNumbers[^1] = remainder;
            }
            else
            {
                for (var i = smaller.Length; i < bigger.Length; i++)
                {
                    var result = bigger[i] + remainder;
                    if (result >= 10)
                    {
                        resultNumbers[i] = result - 10;
                        remainder = 1;
                    }

                    else
                    {
                        resultNumbers[i] = result;
                        remainder = 0;
                    }
                    
                }
            }
        }

        else
        {
            for (var i = smaller.Length; i < bigger.Length; i++)
            {
                resultNumbers[i] = bigger[i];
            }
        }

        if (resultNumbers[^1] == 0)
        {
            var finalNumbers = new int[resultNumbers.Length - 1];

            for (var i = 0; i < finalNumbers.Length; i++)
            {
                finalNumbers[i] = resultNumbers[i];
            }

            for (var i = finalNumbers.Length - 1; i >= 0; i--)
            {
                strNumber += Convert.ToString(finalNumbers[i]);
            }
        }

        else
        {
            var finalNumbers = resultNumbers;

            for (var i = finalNumbers.Length - 1; i >= 0; i--)
            {
                strNumber += Convert.ToString(finalNumbers[i]);
            }
        }

        var resultBigInteger = new BigInteger(strNumber);

        return resultBigInteger;
    }

    private BigInteger Difference(int[] a, int[] b)
    {
        var smaller = a.Length < b.Length ? a : b;
        var bigger = a.Length > b.Length ? a : b;
        var isEqual = a.Length == b.Length ? true : false;
        var isLonger = bigger == a ? true : false;
        var isBigger = true;
        var resultNumbers = new int[bigger.Length + 1];
        var remainder = 0;
        var strNumber = "";

        if (isEqual == true)
        {
            for (var i = a.Length - 1; i >= 0; i--)
            {
                if (a[i] > b[i])
                {
                    isBigger = true;
                    break;
                }

                else if (b[i] > a[i])
                {
                    isBigger = false;
                    break;
                }
            }
        }

        if (isBigger == false || isLonger == false)
        {
            var result = Difference(b, a);
            result.isNegative = true;
            return result;
        }

        for (var i = 0; i < smaller.Length; i++)
        {
            var result = new int();
            
            if (remainder == 0)
            {
                result = a[i] - b[i];
            }

            else
            {
                result = a[i] - b[i] - 1;
            }

            if (result < 0)
            {
                resultNumbers[i] = result + 10;
                remainder = 1;
            }

            else
            {
                resultNumbers[i] = result;
                remainder = 0;
            }
            
            //Console.WriteLine("i: " + i + " rem: " + remainder + " result: " + result);
        }

        if (remainder != 0)
        {
           for (var i = smaller.Length; i < bigger.Length; i++)
           {
               if (isLonger == true)
               {
                   var result = bigger[i] - remainder;

                   if (result < 0)
                   {
                       resultNumbers[i] = result + 10;
                       remainder = 1;
                   }

                   else
                   {
                       resultNumbers[i] = result;
                       remainder = 0;
                   }
               }

               else
               {
                   var result = bigger[i] + remainder;

                   if (result > 10)
                   {
                       resultNumbers[i] = result - 10;
                       remainder = 1;
                   }

                   else
                   {
                       resultNumbers[i] = result;
                       remainder = 0;
                   }
               }
           } 
        }

        else
        {
            for (var i = smaller.Length; i < bigger.Length; i++) 
            { 
                resultNumbers[i] = bigger[i];
            }   
        }

        for (var i = resultNumbers.Length - 1; i >=0; i--)
        {
            strNumber += Convert.ToString(resultNumbers[i]);
        }

        while (Convert.ToString(strNumber[0]) == "0")
        {
            strNumber = strNumber.Remove(0, 1);
        }

        var resultBigInteger = new BigInteger(strNumber);
        
        return resultBigInteger;
    }

    public BigInteger Add(BigInteger another)
    {
        if (isNegative == true && another.isNegative == true)
        {
            var result = Sum(_numbers, another.numbers);
            result.isNegative = true;
            return result;
        }

        else if (another.isNegative == true)
        {
            return Difference(_numbers, another.numbers);
        }

        else if (isNegative == true)
        {
            return Difference(another.numbers, _numbers);
        }

        else
        {
            return Sum(_numbers, another.numbers);
        }
    }

    public BigInteger Sub(BigInteger another)
    {
        if (IsNegative == true && another.isNegative == true)
        {
            return Difference(another.numbers, _numbers);
        }
        
        else if (another.isNegative == true)
        {
            return Sum(_numbers, another.numbers);
        }
        
        else if (isNegative == true)
        {
            var result = Sum(_numbers, another.numbers);
            result.isNegative = true;
            return result;
        }

        else
        {
            return Difference(_numbers, another.numbers);
        }
    }

    private int[] Karatsuba(int[] a, int[] b)
    {
        var m = Math.Min(a.Length, b.Length);
        var m2 = Convert.ToInt32(Math.Floor(Convert.ToDouble(m / 2)));
        
        var low1 = a.Take(m2).ToArray();
        var high1 = a.Skip(m2).ToArray();
        var low2 = b.Take(m2).ToArray();
        var high2 = b.Skip(m2).ToArray();

        var lowhigh1 = Convert.ToString(Convert.ToInt32(String.Join(',', low1)) + Convert.ToInt32(String.Join(',', high1)));
        var lowhigh2 = Convert.ToString(Convert.ToInt32(String.Join(',', low2)) + Convert.ToInt32(String.Join(',', high2)));

        var _lowhigh1 = new int[lowhigh1.Length];
        var _lowhigh2 = new int[lowhigh2.Length];
        
        for (var i = 0; i < lowhigh1.Length; i++)
        {
            _lowhigh1[i] = lowhigh1[i];
        }
        
        for (var i = 0; i < lowhigh2.Length; i++)
        {
            _lowhigh2[i] = lowhigh2[i];
        }
        
        var _z0 = Karatsuba(low1, low2);
        var _z1 = Karatsuba(_lowhigh1, _lowhigh2);
        var _z2 = Karatsuba(high1, high2);

        var z0 = Convert.ToInt32(String.Join(',', _z0));
        var z1 = Convert.ToInt32(String.Join(',', _z1));
        var z2 = Convert.ToInt32(String.Join(',', _z2));

        var result = Convert.ToString(z2 * 10 ^ (m2 * 2)) + ((z1 - z2 - z0) * 10 ^ m2) + z0;
        var _result = new int[result.Length];
        
        for (var i = 0; i < result.Length; i++)
        {
            _result[i] = result[i];
        }

        return _result;
    }

    public BigInteger Multiply(BigInteger another)
    {
        Array.Reverse(_numbers);
        Array.Reverse(another.numbers);
        var _result = Karatsuba(_numbers, another.numbers);
        var result = String.Join(',', _result);
        var bigInteger = new BigInteger(result);
        return bigInteger;
    }
}