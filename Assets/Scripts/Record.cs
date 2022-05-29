using System.IO;

public static class Record {
    public static string LogFilePath = "./";
    public static string LogFileName {
        get {
            int startIdx = 0;
            while (true) {
                if (File.Exists($"{LogFilePath}vr_sound_{startIdx}")) {
                    startIdx ++;
                }
                else {
                    break;
                }
            }
            return $"{LogFilePath}vr_sound_{startIdx}";
        }
    }
}
