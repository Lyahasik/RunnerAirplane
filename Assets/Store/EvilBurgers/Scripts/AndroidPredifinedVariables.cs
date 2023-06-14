namespace EasyHaptic_EvilBurgers
{
    public sealed class AndroidPredifinedVariables
    {
        // Duration on milliseconds
        public static long lightImpactDuration = 40;
        public static long mediumImpactDuration = 75;
        public static long heavyImpactDuration = 120;

        // Amplitude of vibration. Remember if you want add custom variables amplitude must be 'INT' between 0-255 where 0 is 'NO VIBRATION'
        public static int lightImpactAmplitude = 40;
        public static int mediumImpactAmplitude = 120;
        public static int heavyImpactAmplitude = 255;

        // Patterns for wave vibration. You can easly play with values. For better effect need add more than 2 values in array
        public static long[] successPatternDuration = {
                                30,
                                140,
                                30,
                                140
                                };

        public static int[] successPatternAmplitude = {
                                20,
                                75,
                                20,
                                75
                                };
        
        public static long[] warningPatternDuration = { 
                                50, 
                                100,
                                50,
                                100,
                                50,
                                100
                                };

        public static int[] warningPatternAmplitude = { 
                                60, 
                                200,
                                60,
                                200,
                                60,
                                200
                                };

        public static long[] failurePatternDuration = {
                                50,
                                500,
                                50,
                                500
                                };

        public static int[] failurePatternAmplitude = {
                                50,
                                150,
                                50,
                                150 
                                };
    }
}

