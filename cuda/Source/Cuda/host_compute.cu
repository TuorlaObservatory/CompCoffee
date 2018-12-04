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

using namespace std;

void host_compute(const int n)
{
	std::string message{};

	int* host_a = nullptr;
	int* host_b = nullptr;
	int* host_c = nullptr;
	int* threads = nullptr;
	int* blocks = nullptr;


	int* device_a = nullptr;
	int* device_b = nullptr;
	int* device_c = nullptr;
	
	srand(n);

	try
	{

		if (is_failure(
			cudaHostAlloc(&host_a, sizeof(int) * n, cudaHostAllocDefault),
			"cudaHostAlloc",
			&message))
			throw runtime_error(message);
		if (is_failure(
			cudaHostAlloc(&host_b, sizeof(int) * n, cudaHostAllocDefault),
			"cudaHostAlloc",
			&message))
			throw runtime_error(message);
		if (is_failure(
			cudaHostAlloc(&host_c, sizeof(int) * n, cudaHostAllocDefault),
			"cudaHostAlloc",
			&message))
			throw runtime_error(message);
		if (is_failure(
			cudaHostAlloc(&threads, sizeof(int) * n, cudaHostAllocDefault),
			"cudaHostAlloc",
			&message))
			throw runtime_error(message);
		if (is_failure(
			cudaHostAlloc(&blocks, sizeof(int) * n, cudaHostAllocDefault),
			"cudaHostAlloc",
			&message))
			throw runtime_error(message);

		for(auto i = 0 ; i < n; i++)
		{
			host_a[i] = rand() % 100;
			host_b[i] = rand() % 100;
		}

		if (is_failure(
			cudaMalloc(&device_a, sizeof(int) * n),
			"cudaMalloc",
			&message))
			throw runtime_error(message);
		if (is_failure(
			cudaMalloc(&device_b, sizeof(int) * n),
			"cudaMalloc",
			&message))
			throw runtime_error(message);
		if (is_failure(
			cudaMalloc(&device_c, sizeof(int) * n),
			"cudaMalloc",
			&message))
			throw runtime_error(message);

		if (is_failure(
			cudaMemcpy(device_a, host_a, sizeof(int) * n, cudaMemcpyHostToDevice),
			"cudaMemcpy",
			&message))
			throw runtime_error(message);
		if (is_failure(
			cudaMemcpy(device_b, host_b, sizeof(int) * n, cudaMemcpyHostToDevice),
			"cudaMemcpy",
			&message))
			throw runtime_error(message);

		
		// Run code
		kernel<<< n / 8 + 1, 8>>>(device_a, device_b, device_c, n);

		if (is_failure(
			cudaDeviceSynchronize(),
			"cudaDeviceSynchronize",
			&message))
			throw runtime_error(message);

		if (is_failure(
			cudaMemcpy(threads, device_a, sizeof(int) * n, cudaMemcpyDeviceToHost),
			"cudaMemcpy",
			&message))
			throw runtime_error(message);
		if (is_failure(
			cudaMemcpy(blocks, device_b, sizeof(int) * n, cudaMemcpyDeviceToHost),
			"cudaMemcpy",
			&message))
			throw runtime_error(message);
		if (is_failure(
			cudaMemcpy(host_c, device_c, sizeof(int) * n, cudaMemcpyDeviceToHost),
			"cudaMemcpy",
			&message))
			throw runtime_error(message);
		
		for (auto i = 0; i < n; i++)
		{
			cout
				<< setw(3) << host_a[i]
				<< " + "
				<< setw(3) << host_b[i]
				<< " = "
				<< setw(4) << host_c[i]
				<< " on thread " << threads[i]
				<< " in group " << blocks[i]
				<< endl;
		}
		cout << endl;

	}
	catch(exception&)
	{
		if (host_a != nullptr)
			cudaFreeHost(host_a);
		if (host_b != nullptr)
			cudaFreeHost(host_b);
		if (host_c != nullptr)
			cudaFreeHost(host_c);
		if (threads != nullptr)
			cudaFreeHost(threads);
		if (blocks != nullptr)
			cudaFreeHost(blocks);

		if (device_a != nullptr)
			cudaFree(device_a);
		if (device_b != nullptr)
			cudaFree(device_b);
		if (device_c != nullptr)
			cudaFree(device_c);

		throw;
	}

	if (host_a != nullptr)
		cudaFreeHost(host_a);
	if (host_b != nullptr)
		cudaFreeHost(host_b);
	if (host_c != nullptr)
		cudaFreeHost(host_c);
	if (threads != nullptr)
		cudaFreeHost(threads);
	if (blocks != nullptr)
		cudaFreeHost(blocks);

	if (device_a != nullptr)
		cudaFree(device_a);
	if (device_b != nullptr)
		cudaFree(device_b);
	if (device_c != nullptr)
		cudaFree(device_c);
}