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

void example_1(const int n)
{
	// Prints numbers from 0 to n.
	// If n is less than the number of available threads, does it sequentially,
	// otherwise, in parallel with max threads available.
	// Ensures each thread executes at least once.

	const auto is_sequential = n <= omp_get_num_procs();
	
	if (!is_sequential)
		omp_set_num_threads(omp_get_num_procs());

	print_header(is_sequential ? "ex_1_condition_seq" : "ex_1_condition_par", 
			     is_sequential ? 1 : omp_get_num_procs());

	#pragma omp parallel for if(!is_sequential)
	for(auto i = 0 ; i < n; i++)
	{
		#pragma omp critical
		print_out(i, n);
	}

}
