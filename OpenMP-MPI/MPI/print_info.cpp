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

void print_info()
{
	int size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	
	int rank;
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	char name[MPI_MAX_PORT_NAME];

	int name_size;

	MPI_Get_processor_name(name, &name_size);


	std::cout
		<< std::setw(6) << rank
		<< " / "
		<< std::setw(6) << size
		<< " : "
		<< name
		<< std::endl;

}