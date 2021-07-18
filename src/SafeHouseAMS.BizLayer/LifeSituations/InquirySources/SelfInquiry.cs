using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.InquirySources
{
    /// <summary>
    /// Самообращение
    /// </summary>
    public class SelfInquiry : IInquirySource
    {
        /// <summary>
        /// Канал самообращения
        /// </summary>
        [Flags] public enum InquiryChannel
        {
            /// <summary>
            /// не указан
            /// </summary>
            None = 0,

            /// <summary>
            /// Вконтакте
            /// </summary>
            Vk = 1,

            /// <summary>
            /// Фейсбук
            /// </summary>
            Facebook = 2,

            /// <summary>
            /// Инстаграмм
            /// </summary>
            Instagramm = 4,

            /// <summary>
            /// Почта
            /// </summary>
            Email = 8,

            /// <summary>
            /// Телефон
            /// </summary>
            Phone = 16,

            /// <summary>
            /// Вотсапп
            /// </summary>
            Whatsapp = 32,

            /// <summary>
            /// Вайбер
            /// </summary>
            Viber = 64,

            /// <summary>
            /// Телеграмм
            /// </summary>
            Telegramm = 128,

            /// <summary>
            /// Сигнал
            /// </summary>
            Signal = 256
        }

        /// <summary>
        /// Собственно канал самообращения 
        /// </summary>
        public InquiryChannel Channel { get; }

        /// <summary>
        /// Самообращение
        /// </summary>
        /// <param name="channel">Канал самообращения</param>
        public SelfInquiry(InquiryChannel channel)
        {
            Channel = channel;
        }
    }
}