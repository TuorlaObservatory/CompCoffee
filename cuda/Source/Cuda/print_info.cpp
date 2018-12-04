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

void print_info()
{
	auto count = 0;

	string message = {};
	if (is_failure(
		cudaGetDeviceCount(&count),
		"cudaGetDeviceCount",
		&message))
		throw runtime_error(message);

	auto current_dev = -1;
	if (is_failure(
		cudaGetDevice(&current_dev), 
		"cudaGetDevice",
		&message))
		throw runtime_error(message);

	cudaDeviceProp properties {};

	if (is_failure(
		cudaGetDeviceProperties(&properties, current_dev),
		"cudaGetDeviceProperties",
		&message))
		throw runtime_error(message);

	cout
		<< setw(30) << left << "Name " << properties.name << "\r\n"
		<< setw(30) << left << "Memory " << properties.totalGlobalMem / 1024 / 1024 << " Mib\r\n"
		<< setw(30) << left << "Warp size " << properties.warpSize << "\r\n"
		<< setw(30) << left << "Max thread dim " << properties.maxThreadsPerBlock << "\r\n"
		<< setw(30) << left << "Threads per block "
			<< properties.maxThreadsDim[0] << " x "
			<< properties.maxThreadsDim[1] << " x "
			<< properties.maxThreadsDim[2] << "\r\n"
		<< setw(30) << left << "Grid size "
			<< properties.maxGridSize[0] << " x "
			<< properties.maxGridSize[1] << " x "
			<< properties.maxGridSize[2] << "\r\n"
		<< endl;


}