using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _52_3
{
    internal class Program
    {
        // 定義一個抽象類別，作為貸款與存款計算器的基底類別
        public abstract class RateCalculator
        {
            // 抽象方法，強制子類別必須實作此方法
            public abstract decimal Calculate(decimal principal, CustomerType customerType);
        }
        static void Main(string[] args)
        {
            try
            {
                // 提示使用者輸入本金
                Console.WriteLine("請輸入本金：");
                // 讀取使用者輸入，並轉換為小數
                decimal principal = decimal.Parse(Console.ReadLine());

                // 建立 LoanAndSavingCalculator 的實例
                var calculator = new LoanAndSavingCalculator();

                // 使用迴圈列舉 CustomerType 的所有值
                foreach (CustomerType customerType in Enum.GetValues(typeof(CustomerType)))
                {
                    // 呼叫計算器的 Calculate 方法，計算並顯示結果
                    calculator.Calculate(principal, customerType);
                }
            }
            catch (FormatException)
            {
                // 捕捉格式錯誤的例外，提示使用者輸入格式不正確
                Console.WriteLine("輸入格式不正確，請輸入數字作為本金。");
            }
            catch (Exception ex)
            {
                // 捕捉其他例外，顯示錯誤訊息
                Console.WriteLine($"發生錯誤：{ex.Message}");
            }
        }

        // LoanAndSavingCalculator 繼承 RateCalculator，實現貸款和存款的本利和計算
        public class LoanAndSavingCalculator : RateCalculator
        {
            // 覆寫抽象方法 Calculate
            public override decimal Calculate(decimal principal, CustomerType customerType)
            {
                decimal loanRate;  // 貸款利率
                decimal savingRate;  // 存款利率

                // 根據客戶類型設定不同的利率
                switch (customerType)
                {
                    case CustomerType.Standard:
                        loanRate = 0.09m;  // 一般市場貸款利率 9%
                        savingRate = 0.06m;  // 一般市場存款利率 6%
                        break;
                    case CustomerType.SmallAndMediumBusiness:
                        loanRate = 0.07m;  // 中小企業貸款利率 7%
                        savingRate = 0.035m;  // 中小企業存款利率 3.5%
                        break;
                    case CustomerType.Enterprise:
                        loanRate = 0.06m;  // 企業貸款利率 6%
                        savingRate = 0.03m;  // 企業存款利率 3%
                        break;
                    case CustomerType.Government:
                        loanRate = 0.03m;  // 政府貸款利率 3%
                        savingRate = 0.02m;  // 政府存款利率 2%
                        break;
                    default:
                        // 如果傳入的客戶類型無法處理，拋出例外
                        throw new NotSupportedException("不支援的客戶類型");
                }

                // 計算貸款本利和 = 本金 * (1 + 貸款利率)
                decimal loanAmount = principal * (1 + loanRate);
                // 計算存款本利和 = 本金 * (1 + 存款利率)
                decimal savingAmount = principal * (1 + savingRate);

                // 輸出計算結果
                Console.WriteLine($"客戶類型：{customerType}");
                Console.WriteLine($"貸款本利和：{loanAmount:C}");  // 格式化為貨幣格式
                Console.WriteLine($"存款本利和：{savingAmount:C}");
                Console.WriteLine();
                return 0; // 此方法僅用於輸出數據，不需返回實際值
            }
        }
        // 定義客戶類型的列舉值，包含四種類型
        public enum CustomerType
        {
            Standard,  // 一般市場
            SmallAndMediumBusiness,  // 中小企業
            Enterprise,  // 企業
            Government  // 政府
        }
    }
}
