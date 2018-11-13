// Copyright 2018 Ilia Kosenkov (Tuorla Observatory, Finland) ilia.kosenkov.at.gm@gmail.com
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of 
// this software and associated documentation files(the "Software"), to deal 
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or 
// sell copies of the Software, and to permit persons to whom the Software 
// is furnished to do so, subject to the following conditions :
// 
// The above copyright notice and this permission notice shall be included 
// in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT
// SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#include "Includes.h"


int main(int argc, char** argv)
{
	print_threads(omp_get_num_procs());
	
	shared_loop_1(3, 10);
	shared_loop_2(7, 29);
	shared_loop_3(7, 29);

	example_1(std::max(omp_get_num_procs() - 1, 1));
	example_1(16);
	example_2(13);
	example_3(7, 15);
	example_4(5);

	#ifdef _MSC_VER
	system("PAUSE");
	#endif

	return 0;
}