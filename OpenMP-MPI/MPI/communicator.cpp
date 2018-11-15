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

void communicator()
{
	int rank;
	throw_if(MPI_Comm_rank(MPI_COMM_WORLD, &rank), "MPI_Comm_rank");

	int size;
	throw_if(MPI_Comm_size(MPI_COMM_WORLD, &size), "MPI_Comm_size");
	
	if(rank == 0)
	{
			for(auto counter = 0; counter < size  * (size - 1) / 2;)
			{
				MPI_Status status;
				throw_if(MPI_Probe(MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, &status), "MPI_Probe");
				
				int count;
				throw_if(MPI_Get_count(&status, MPI_CHAR, &count), "MPI_Get_count");

				const auto data = new byte[count];
				throw_if(MPI_Recv(data, count, MPI_CHAR, status.MPI_SOURCE, status.MPI_TAG, MPI_COMM_WORLD, MPI_STATUS_IGNORE), "MPI_Recv");

				int sender_rank;
				memcpy(&sender_rank, data, sizeof(int));

				const auto offset = sizeof(int) / sizeof(byte);
				std::string message{ data + offset, count - offset };

				if (message == "exit")
					counter += sender_rank;
				else
					std::cout
					<< '['
					<< std::setw(2)
					<< sender_rank
					<< "]: "
					<< message << std::endl;

				delete[] data;

			}
	}
	else
	{
		std::ostringstream str;
		str << "Hello from proc " << rank << "!";
		send_message(str.str(), rank);

		srand(rank);

		for (auto index = 0; index <= rank / 4; index++)
		{
			std::this_thread::sleep_for(std::chrono::milliseconds(rand() % 1000));

			str.str(std::string{});
			str.clear();
			str << "Here is a random number: " << rand() % 1000;
			send_message(str.str(), rank);
		}

		send_message("exit", rank);

	}

}