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

void shared_loop_1(const int n, const int total)
{
	omp_set_num_threads(n);

	print_header("sh_loop_1_lock", n);

	const auto by = static_cast<int>(std::ceil(1.0 * total / n));

	omp_lock_t lock;
	omp_init_lock(&lock);

	#pragma omp parallel
	{
		const auto start = omp_get_thread_num() * by;
		auto end = (omp_get_thread_num() + 1) * by;
		if (omp_get_thread_num() == (omp_get_num_threads() - 1))
			end = total;

		for (auto j = start; j < end && j < total; j++)
		{
			omp_set_lock(&lock);
			print_out(j, total);
			omp_unset_lock(&lock);
		}

	}

	omp_destroy_lock(&lock);

}