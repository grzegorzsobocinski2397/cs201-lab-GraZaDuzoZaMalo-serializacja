namespace Serializacja.Encryption
{
    /// <summary>
    /// Contains Encryption Key and Initialization Vector required for XML data encryption.
    /// </summary>
    public static class EncryptionValues
    {
        /// <summary>
        /// Encryption Key just for presenting that XML encryption works.
        /// </summary>
        public static readonly byte[] EncryptionKey = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 };

        /// <summary>
        /// 'Random' number used for encryption.
        /// </summary>
        public static readonly byte[] EncryptionInitializationVector = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };
    }
}