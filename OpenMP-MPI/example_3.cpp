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

void example_3(const int n, const int total)
{
	
	omp_set_num_threads(n);

	print_header("ex_3_sums", n);

	auto sum = 0.0;
	auto sum_explicit = 0.0;

	#pragma omp parallel for reduction(+ : sum) schedule(dynamic, 2)
	for(auto i = 0 ;  i < total; i++)
	{
		sum += i + 1;
	
		#pragma omp critical
		print_out(i, total);
	}

	std::cout << std::endl;

	#pragma omp parallel for shared(sum_explicit)
	for (auto i = 0; i < total; i++)
	{
		#pragma omp atomic
		sum_explicit += i + 1;
		
		#pragma omp critical
		print_out(i, total);
	}

	std::cout
		<< "Exact sum:\t"
		<< std::setw(10)
		<< (1 + total) * total / 2
		<< std::endl
		<< "Parallel sum:\t"
		<< std::setw(10)
		<< sum
		<< std::endl
		<< "Parallel2 sum:\t"
		<< std::setw(10)
		<< sum_explicit
		<< std::endl;
}