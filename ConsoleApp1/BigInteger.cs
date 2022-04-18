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
    
    public bool IsNegative()
    {
        return isNegative;
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

    public BigInteger Add(BigInteger another)
    {
        var smaller = _numbers.Length < another.numbers.Length ? _numbers : another.numbers;
        var bigger = _numbers.Length > another.numbers.Length ? _numbers : another.numbers;
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

            for (var i = finalNumbers.Length - 1; i >=0; i--)
            {
                strNumber += Convert.ToString(finalNumbers[i]);
            }
        }

        else
        {
            var finalNumbers = resultNumbers;

            for (var i = finalNumbers.Length - 1; i >=0; i--)
            {
                strNumber += Convert.ToChar(finalNumbers[i]);
            }
        }

        var resultBigInteger = new BigInteger(strNumber);
        
        return resultBigInteger;
    }

    public BigInteger Sub(BigInteger another)
    {
        var smaller = _numbers.Length < another.numbers.Length ? _numbers : another.numbers;
        var bigger = _numbers.Length > another.numbers.Length ? _numbers : another.numbers;
        var isBigger = bigger == _numbers ? true : false;
        var resultNumbers = new int[bigger.Length + 1];
        var remainder = 0;
        var strNumber = "";

        for (var i = 0; i < smaller.Length; i++)
        {
            var result = new int();
            
            if (remainder == 0)
            {
                result = _numbers[i] - another.numbers[i];
            }

            else
            {
                result = _numbers[i] - another.numbers[i] - 1;
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
               if (isBigger == true)
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
            strNumber.Remove(0);
        }

        var resultBigInteger = new BigInteger(strNumber);
        
        return resultBigInteger;    
    }
}