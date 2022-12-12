namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        private readonly double re;
        private readonly double im;
        public static double Tollerance { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real => this.Real;

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary => this.Imaginary;

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus => Math.Sqrt(this.re * this.re + this.im * this.im);

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase => Math.Atan2(this.im, this.re);

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            // TODO improve
            return base.ToString();
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other)
        {
            return other != null && (this.Imaginary - other.Imaginary) < Complex.Tollerance && this.Real == other.Real;
        }

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            if(obj is IComplex)
            {
                return this.Equals((IComplex)obj);
            }

            return false;
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.re, this.im);
        }
    }
}
