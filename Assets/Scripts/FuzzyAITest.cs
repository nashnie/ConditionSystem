using UnityEngine;

using AForge.Fuzzy;

public class FuzzyAITest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       TrapezoidalFunction function1 = new TrapezoidalFunction(
           10, 15, TrapezoidalFunction.EdgeType.Right );
       FuzzySet fsCold = new FuzzySet( "Cold", function1 );
       TrapezoidalFunction function2 = new TrapezoidalFunction( 10, 15, 20, 25 );
       FuzzySet fsCool = new FuzzySet( "Cool", function2 );
       TrapezoidalFunction function3 = new TrapezoidalFunction( 20, 25, 30, 35 );
       FuzzySet fsWarm = new FuzzySet( "Warm", function3 );
       TrapezoidalFunction function4 = new TrapezoidalFunction(
           30, 35, TrapezoidalFunction.EdgeType.Left );
       FuzzySet fsHot = new FuzzySet( "Hot", function4 );
       
       // create a linguistic variable to represent steel temperature
       LinguisticVariable lvSteel = new LinguisticVariable( "Steel", 0, 80 );
       // adding labels to the variable
       lvSteel.AddLabel( fsCold );
       lvSteel.AddLabel( fsCool );
       lvSteel.AddLabel( fsWarm );
       lvSteel.AddLabel( fsHot );
       
       // create a linguistic variable to represent stove temperature
       LinguisticVariable lvStove = new LinguisticVariable( "Stove", 0, 80 );
       // adding labels to the variable
       lvStove.AddLabel( fsCold );
       lvStove.AddLabel( fsCool );
       lvStove.AddLabel( fsWarm );
       lvStove.AddLabel( fsHot );
       
       // create the linguistic labels (fuzzy sets) that compose the pressure
       TrapezoidalFunction function5 = new TrapezoidalFunction(
           20, 40, TrapezoidalFunction.EdgeType.Right );
       FuzzySet fsLow = new FuzzySet( "Low", function5 );
       TrapezoidalFunction function6 = new TrapezoidalFunction( 20, 40, 60, 80 );
       FuzzySet fsMedium = new FuzzySet( "Medium", function6 );
       TrapezoidalFunction function7 = new TrapezoidalFunction(
           60, 80, TrapezoidalFunction.EdgeType.Left );
       FuzzySet fsHigh = new FuzzySet( "High", function7 );
       // create a linguistic variable to represent pressure
       LinguisticVariable lvPressure = new LinguisticVariable( "Pressure", 0, 100 );
       // adding labels to the variable
       lvPressure.AddLabel( fsLow );
       lvPressure.AddLabel( fsMedium );
       lvPressure.AddLabel( fsHigh );
       
       // create a linguistic variable database
       Database db = new Database( );
       db.AddVariable( lvSteel );
       db.AddVariable( lvStove );
       db.AddVariable( lvPressure );
       
       // sample rules just to test the expression parsing
       Rule r1 = new Rule( db, "Test1", "IF Steel is not Cold and Stove is Hot then Pressure is Low" );
       Rule r2 = new Rule( db, "Test2", "IF Steel is Cold and not (Stove is Warm or Stove is Hot) then Pressure is Medium" );
       Rule r3 = new Rule( db, "Test3", "IF Steel is Cold and Stove is Warm or Stove is Hot then Pressure is High" );
       
       // testing the firing strength
       lvSteel.NumericInput = 12;
       lvStove.NumericInput = 35;
       float result = r1.EvaluateFiringStrength( );

       Debug.Log(result.ToString());
    }
}
