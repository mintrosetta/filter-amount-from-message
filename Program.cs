using System.Diagnostics;

namespace Program
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<string> messages = new List<string>()
            {
                "หักคอมฝ่ายขาย 1799.74  บาทขอบคุณค่ะ",
                "หักคอมฝ่ายขาย 2000 บ.ขอบคุณค่ะ",
                "หักคอมฝ่ายขาย SALE.BALANCE บาท ขอบคุณค่ะ",
                "รบกวนหักคอมค่าคุ้มครอง (กธ.) ยอด  543.56 บาท  06-06-63",
                "รบกวนหักคอมค่าคุ้มครอง (พรบ.) ยอด  196.21  บาท วันที่ 15/07/63",
                "รบกวนหักคอมค่าคุ้มครอง (กธ.) ยอด  7,752.20  02/07/2020",
                "รบกวนหักคอมค่าคุ้มครอง (กธ.) ยอด   344.54 บาท  11-07-63",
                "หักคอมฝ่ายขาย  8,854.37  บาท ขอบคุณค่ะ"
            };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (string message in messages)
            {
                Console.WriteLine($"Remark: {message}");
                Console.WriteLine($"Amount: {Solve(message)}\n");
            }

            stopwatch.Stop();
            Console.WriteLine($"Compile Time: {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
        }

        public static decimal Solve(string message)
        {
            if (message.Length == 0) return 0;

            string amountText = string.Empty;
            for (int messageIndex = 0; messageIndex < message.Length; messageIndex++)
            {
                char character = message[messageIndex];

                // เงื่อนไข ถ้าตัวอักษรอยู่ระหว่าง 0 - 9
                bool numberCondition = (character >= '0' && character <= '9');

                // เงื่อนไข ถ้าตัวอักษรเป็น '.'
                bool dotCondition = (
                    (character == '.' || character == ',')
                    && (message[messageIndex - 1] >= '0' && message[messageIndex - 1] <= '9') // ตำแหน่งก่อนหน้า '.' เป็นตัวเลข
                    && messageIndex + 1 < message.Length // index ถัดไปจะต้องน้อยกว่าขนาดของ message
                    && (message[messageIndex + 1] >= '0' && message[messageIndex + 1] <= '9') // ตำแหน่งถัดไปจะต้องเป็นตัวอักษร
                );

                bool nextIsNumberOrDotCondition = (
                    messageIndex + 1 < message.Length 
                    && (
                            (message[messageIndex + 1] >= '0' && message[messageIndex + 1] <= '9') 
                            || message[messageIndex + 1] == '.' 
                            || message[messageIndex + 1] == ','
                        )
                );

                
                if (numberCondition) // ถ้าอยู๋ระหว่าง 0 - 9
                {
                    // ถ้าหากตำแหน่งของตัวเลข เท่ากับ ตัวสุดท้ายของ message ให้เพิ่มลงไปใน amountText เลย
                    if (messageIndex == message.Length - 1)
                    {
                        amountText += character;
                    }
                    else 
                    {
                        // ถ้า ตัวอักษร ไม่ใช่ '.' ให้้ทำการเพิ่มตัวอักษรนั้นลงไปใน amountText
                        if (message[messageIndex + 1] != '.')
                        {   
                            amountText += character;
                        }
                        else 
                        {
                            // ถ้า ตัวอักษร ตำแหน่งถัดไปต่อจาก '.' เป็นตัวเลข ให้เพิ่ม text ลงไปใน amountText
                            if ((messageIndex + 2) <= message.Length && message[messageIndex + 2] >= '0' && message[messageIndex + 2] <= '9') amountText += character;
                        }
                    }

                    if (!nextIsNumberOrDotCondition) break;
                }
                else if (dotCondition) // ถ้าเป็น '.'
                {
                    amountText += character; // เพิ่มตัวอักษรลง amountText ไปเลย
                }
            }

            // ถ้า amountText เป็นค่าว่าง ให้ส่งคืน 0, ถ้าไม่ใช่ให้แปลงเป็น decimal และส่งคืน
            return (string.IsNullOrEmpty(amountText)) ? 0 : Convert.ToDecimal(amountText);
        }
    }
}