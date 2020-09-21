/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
 
using SwiftPSO.Benchmarks;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Problem.Decorator;
using SwiftPSO.Core.Types;
using SwiftPSO.Core.Fitness;
using SwiftPSO.Core.ControlParameter;
using SwiftPSO.FLA.Metrics;
using SwiftPSO.FLA.Sampling;
using SwiftPSO.Simulator;
using System;
using System.Diagnostics;
using System.Collections;
/*
This file is generates output which can be used to sanity check other implementations. 
*/
namespace SwiftPSO.FLA.Test {
    class GenerateMetricOutput {
        public static void Main() {
            new GenerateMetricOutput().run();
        }
        private void run()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //We assume that the functions have been previously validated, and select a domain valid for all considered functions 
            int dimensions = 5;
            Domain dom = new Domain(0.25,0.5, dimensions);
            Bounds[] bounds = new Bounds[dimensions];
            for(int i = 0; i < dimensions; i++)
                bounds[i] = new Bounds(0.25, 0.5);
            int sampleSize = 500;
            int runs = 30; 
            int funcs = 40;
            double stepSize = 0.01;

            OptimizationSolution[][][] samples = new OptimizationSolution[runs][][];
            for(int i = 0; i < runs; i++){
                samples[i] = new OptimizationSolution[funcs][];
                for(int j = 0; j < funcs; j++)
                    samples[i][j] = new OptimizationSolution[sampleSize];
            }

            for(int i = 0; i < runs; i++) {

                //define the problems to optimize
                //redefine the problem suite each run, since we want different rotation matrices for each run 
                //There is a TODO in problem suite regarding whether or not calls to Func return new instances. For the sake of this testing it seemed safer to just re-recreate the entire problem suite for each run, rather than make assumptions.
                ProblemSuite problemSuite = new ProblemSuite();

                //F1
                problemSuite.AddFunction(() => new AbsoluteValue(), new Domain(-100, 100, dimensions), "F1");

                //F2
                problemSuite.AddFunction(() => new Ackley(), new Domain(-32.768, 32.768, dimensions), "F2");

                problemSuite.AddShiftedFunction(() => new Ackley(), new ConstantControlParameter(10), new ConstantControlParameter(-140), new Domain(-32.768, 32.768, dimensions), "F3");

                problemSuite.AddRotatedFunction(() => new Ackley(), RotationType.Orthonormal, new Domain(-32.768, 32.768, dimensions), "F4");

                problemSuite.AddShiftedRotatedFunction(() => new Ackley(), new ConstantControlParameter(-32), new ConstantControlParameter(-140), RotationType.LinearTransform, 100, new Domain(-32.768, 32.768, dimensions), "F5");

                //F3
                problemSuite.AddFunction(() => new Alpine(), new Domain(-100, 100, dimensions), "F6");

                //F4
                problemSuite.AddFunction(() => new EggHolder(), new Domain(-100, 100, dimensions), "F7");

                //F5
                problemSuite.AddFunction(() => new Elliptic(), new Domain(-100,100, dimensions), "F8");

                problemSuite.AddShiftedFunction(() => new Elliptic(),new ConstantControlParameter(10),new ConstantControlParameter(-450),  new Domain(-100,100, dimensions), "F9");

                problemSuite.AddRotatedFunction(() => new Elliptic(), RotationType.Orthonormal, new Domain(-100,100, dimensions), "F10");

                problemSuite.AddShiftedRotatedFunction(() => new Elliptic(),new ConstantControlParameter(-10), new ConstantControlParameter(-450),  RotationType.Orthonormal, 1, new Domain(-100, 100, dimensions), "F11");

                //F6
                problemSuite.AddFunction(() => new Griewank(), new Domain(-600,600, dimensions), "F12");

                problemSuite.AddShiftedFunction(() => new Griewank(),new ConstantControlParameter(10),new ConstantControlParameter(-180),  new Domain(-600,600, dimensions), "F13");

                problemSuite.AddRotatedFunction(() => new Griewank(), RotationType.Orthonormal, new Domain(-600,600, dimensions), "F14");

                problemSuite.AddShiftedRotatedFunction(() => new Griewank(),new ConstantControlParameter(-60), new ConstantControlParameter(-180),  RotationType.LinearTransform, 3, new Domain(0, 600, dimensions), "F15");

                //F7
                problemSuite.AddFunction(() => new HyperEllipsoid(), new Domain(-5.12, 5.12, dimensions), "F16");

                //F8
                problemSuite.AddFunction(() => new Michalewicz(), new Domain(0, 3.14159, dimensions), "F17");

                //F9
                problemSuite.AddFunction(() => new Norwegian(), new Domain(-1.1, 1.1, dimensions), "F18");

                //F10
                problemSuite.AddFunction(() => new Quadric(), new Domain(-100, 100, dimensions), "F19");

                //F11
                problemSuite.AddFunction(() => new Quartic(), new Domain(-1.28, 1.28, dimensions), "F20");

                //Note that according to engelbrechts paper says the noise is added INSIDE the sumation, not out, so we use a large scale here to compensate
                problemSuite.AddFunction(() => new DeJongF4(),new Domain(-1.28, 1.28, dimensions), "F21");

                //F12
                problemSuite.AddFunction(() => new Rastrigin(), new Domain(-5.12,5.12, dimensions), "F22");

                problemSuite.AddShiftedFunction(() => new Rastrigin(),new ConstantControlParameter(2),new ConstantControlParameter(-330),  new Domain(-5.12,5.12, dimensions), "F23");

                problemSuite.AddRotatedFunction(() => new Rastrigin(), RotationType.Orthonormal, new Domain(-5.12,5.12, dimensions), "F24");

                problemSuite.AddShiftedRotatedFunction(() => new Rastrigin(),new ConstantControlParameter(1), new ConstantControlParameter(-330),  RotationType.LinearTransform, 2, new Domain(-5.12,5.12, dimensions), "F25");

                //F13 
                problemSuite.AddFunction(() => new Rosenbrock(), new Domain(-30,30, dimensions), "F26");

                problemSuite.AddShiftedFunction(() => new Rosenbrock(),new ConstantControlParameter(10),new ConstantControlParameter(390),  new Domain(-100,100, dimensions), "F27");

                problemSuite.AddRotatedFunction(() => new Rosenbrock(), RotationType.Orthonormal, new Domain(-100,100, dimensions), "F28");

                //F14
                problemSuite.AddFunction(() => new Salomon(), new Domain(-100,100, dimensions), "F29");

                //F15
                problemSuite.AddFunction(() => new Schaffer6(), new Domain(-100,100, dimensions), "F30");

                problemSuite.AddShiftedRotatedFunction(() => new Schaffer6(),new ConstantControlParameter(20), new ConstantControlParameter(-300),  RotationType.LinearTransform, 3, new Domain(-100,100, dimensions), "F31");

                //F16 //REMEMBER THIS IS INCORRECT
                problemSuite.AddFunction(() => new Rana(), new Domain(-100,100, dimensions), "F32");
                //NOT THE RIGHT FUNC
                problemSuite.AddShiftedFunction(() => new Rana(),new ConstantControlParameter(10),new ConstantControlParameter(-450),  new Domain(-100,100, dimensions), "F33");
                //NOT THE RIGHT FUNC
                problemSuite.AddRotatedFunction(() => new Rana(), RotationType.Orthonormal, new Domain(-100,100, dimensions), "F34");
                //NOT THE RIGHT FUNC
                problemSuite.AddFunction(() => new NoisyDecorator{
                    Function = new ShiftDecorator(new Rana(),new ConstantControlParameter(-450),new ConstantControlParameter(10)),
                    Scale =  new ConstantControlParameter(0.4),
                    Offset = new ConstantControlParameter(0.0),
                }, new Domain(-100, 100, dimensions), "F35");

                //F17
                problemSuite.AddFunction(() => new Schwefel2_6(), new Domain(-100,100, dimensions), "F36");

                problemSuite.AddShiftedFunction(() => new Schwefel2_6(),new ConstantControlParameter(0),new ConstantControlParameter(-310), new Domain(-100,100, dimensions), "F37");

                //F18
                problemSuite.AddFunction(() => new Schwefel2_13(), new Domain(-3.14159,3.14159, dimensions), "F38");

                problemSuite.AddShiftedFunction(() => new Schwefel2_13(),new ConstantControlParameter(0),new ConstantControlParameter(-460),  new Domain(-3.14159,3.14159, dimensions), "F39");

                //F19
                problemSuite.AddFunction(() => new Schwefel2_21(), new Domain(-100,100, dimensions), "F40");

                //F20
                problemSuite.AddFunction(() => new Schwefel2_22(), new Domain(-10,10, dimensions), "F41");

                //F21
                problemSuite.AddFunction(() => new Shubert(), new Domain(-10,10, dimensions), "F42");

                //F22
                problemSuite.AddFunction(() => new Spherical(), new Domain(-5.12,5.12, dimensions), "F43");

                problemSuite.AddShiftedFunction(() => new Spherical(),new ConstantControlParameter(10),new ConstantControlParameter(-450),  new Domain(-5.12,5.12, dimensions), "F44");

                //F23
                problemSuite.AddFunction(() => new Step(), new Domain(-100,100, dimensions), "F45");

                //F24
                problemSuite.AddFunction(() => new Vincent(), new Domain(0.25,10, dimensions), "F46");

                //F25
                problemSuite.AddFunction(() => new Weierstrass(), new Domain(-0.5,0.5, dimensions), "F47");

                problemSuite.AddShiftedRotatedFunction(() => new Weierstrass(),new ConstantControlParameter(0.1), new ConstantControlParameter(90),  RotationType.LinearTransform, 5, new Domain(-0.5,0.5, dimensions), "F48");


                //Generate a uniform random sample

                ManhattanProgressiveRandomWalk ManProgSamp = new ManhattanProgressiveRandomWalk();
                BitArray startZone = new BitArray(dimensions);
                for(int x = 0; x < dimensions; x++) {
                    if(i + x %3 == 0) startZone.Set(x, true);
                }
                
                

                Vector[] sample = ManProgSamp.Walk(sampleSize, dom, startZone, stepSize);
                
                //Evaluate each member of the sample using each function to generate OptimizedSolutions
                for(int fNum = 1; fNum < funcs; fNum++)
                {
                    IProblem p = problemSuite.Problems["F"+fNum]();

                    for(int sampleIndex = 0; sampleIndex < sampleSize; sampleIndex++) 
                    {
                        IFitness f = p.Evaluate(sample[sampleIndex]);

                        samples[i][fNum][sampleIndex] = new OptimizationSolution(sample[sampleIndex], f);
                    }
                }

            }

            stopwatch.Stop();
            Console.WriteLine($"Elapsed Time: {stopwatch.Elapsed}");

            //Now we have a conveniently iterable sample for each function for each run, so we start getting output from each metric

            //FEM 
            Console.WriteLine("//FEM");
            FirstEntropicMeasure FEM = new FirstEntropicMeasure();
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(FEM.Calculate(samples[j][i]));
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            //FDC
            Console.WriteLine("//FDC");
            FitnessDistanceCorrelation FDC = new FitnessDistanceCorrelation();
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(FDC.Calculate(samples[j][i]));
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            //Gradient 
            Matrix aves = new Matrix(funcs, runs);
            Matrix devs = new Matrix(funcs,runs);
            Gradient Grad = new Gradient();
            for(int i = 1; i < funcs; i++) {
                for(int j = 0; j < runs; j++) {
                    (double ave, double dev) = Grad.Calculate(samples[j][i], stepSize, dom);
                    aves[i,j] = ave;
                    devs[i,j] = dev;
                }                    
            }

            Console.WriteLine("//Gradient Aves");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(aves[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            Console.WriteLine("//Gradient Standard Deviations");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(devs[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            //M Measure
            Matrix M1 = new Matrix(funcs, runs);
            Matrix M2 = new Matrix(funcs,runs);
            MMeasure mMeas = new MMeasure();
            for(int i = 1; i < funcs; i++) {
                for(int j = 0; j < runs; j++) {
                    (double m1, double m2) = mMeas.Calculate(samples[j][i]);
                    M1[i,j] = m1;
                    M2[i,j] = m2;
                }                    
            }

            Console.WriteLine("//M1");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(M1[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            Console.WriteLine("//M2");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(M2[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");


            //Stagnation
            Matrix StagL = new Matrix(funcs, runs);
            Matrix StagN = new Matrix(funcs,runs);
            Stag stag = new Stag();
            for(int i = 1; i < funcs; i++) {
                for(int j = 0; j < runs; j++) {
                    (double stagL, double stagN) = stag.Calculate(samples[j][i]);
                    StagL[i,j] = stagL;
                    StagN[i,j] = stagN;
                }                    
            }

            Console.WriteLine("//StagL");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(StagL[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            Console.WriteLine("//StagN");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(StagN[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            //Fitness Distribution
            Matrix Skew = new Matrix(funcs, runs);
            Matrix Kurtosis = new Matrix(funcs,runs);
            FitnessDistribution fitDist = new FitnessDistribution();
            for(int i = 1; i < funcs; i++) {
                for(int j = 0; j < runs; j++) {
                    (double skew, double kurtosis) = fitDist.Calculate(samples[j][i]);
                    Skew[i,j] = skew;
                    Kurtosis[i,j] = kurtosis;
                }                    
            }

            Console.WriteLine("//Skew");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(Skew[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            Console.WriteLine("//Kurtosis");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(Kurtosis[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            //Dispersion 
            //TODO: remember you'll need to adjust the parameters to to make this one equivalent to mine
            //TODO: remember Kyle normalized his positions before calculating the pairwise distance, which I didn't do
            //for each sample only keep the 0.15 % best??
            Console.WriteLine("//Dispersion");
            DispersionMetric disp = new DispersionMetric();
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(disp.Calculate(samples[j][i], bounds, 0.20));
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            //Nearest Neighbor Features
            //TODO: remember the distances were normalized in this implementation, but not in the c++ one
            Matrix NNF1 = new Matrix(funcs, runs);
            Matrix NNF2 = new Matrix(funcs, runs);
            Matrix NNF3 = new Matrix(funcs, runs);
            Matrix NNF4 = new Matrix(funcs, runs);
            Matrix NNF5 = new Matrix(funcs, runs);

            NearestNeighborFeatures NNFeat = new NearestNeighborFeatures();
            for(int i = 1; i < funcs; i++) {
                for(int j = 0; j < runs; j++) {
                    (double nnf1, double nnf2, double nnf3, double nnf4, double nnf5) = NNFeat.Calculate(samples[j][i], bounds);
                    NNF1[i,j] = nnf1;
                    NNF2[i,j] = nnf2;
                    NNF3[i,j] = nnf3;
                    NNF4[i,j] = nnf4;
                    NNF5[i,j] = nnf5;
                }                    
            }

            Console.WriteLine("//NNF1");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(NNF1[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            Console.WriteLine("//NNF2");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(NNF2[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            Console.WriteLine("//NNF3");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(NNF3[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            Console.WriteLine("//NNF4");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(NNF4[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

            Console.WriteLine("//NNF5");
            Console.Write("{");
            for(int i = 1; i < funcs; i++) {
                Console.Write("{");
                for(int j = 0; j < runs; j++) {
                    Console.Write(NNF5[i,j]);
                    if(j < runs-1)
                        Console.Write(",");
                    else
                        Console.Write("}");
                }
                if(i < funcs-1)
                    Console.WriteLine(",");                      
            }
            Console.WriteLine("}");

        }
    } 
}