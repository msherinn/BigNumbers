using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1;

public class BigInteger
{
    private int[] _numbers;
    private bool isNegative = false;

    public BigInteger(string value)
    {
        _numbers = new int[value.Length];

        if (value[0] == '-')
        {
            isNegative = true;
            value.Remove('-');
        }
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
        var smaller = a.Length < b.Length ? a : b;
        var bigger = a.Length > b.Length ? a : b;
        var resultNumbers = new int[bigger.Length + 1];
        var remainder = 0;
        var strNumber = "";
        
        for (var i = 0; i < smaller.Length; i++)
        {
            var result = new int();
            if (remainder == 0)
            {
                result = bigger[i] + smaller[i];
            }

            else
            {
                result = bigger[i] + smaller[i] + 1;
            }

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
                strNumber += Convert.ToChar(finalNumbers[i]);
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
            var i = a.Length - 1;
            while (isBigger == true && i >= 0)
            {
                isBigger = a[i] >= b[i] ? true : false;
                i--;
            }

            if (isBigger == false)
            {
                var result = Difference(b, a);
                result.isNegative = true;
                return result;
            }
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
}