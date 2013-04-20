====================================================================================
HostResolver

Current version: 1.0.0

Send comments and bug reports to: nick.dunn@nccgroup.com

====================================================================================

Contents:

1. Overview
2. Using HostResolver
	Input files
	Options & settings
	Scanning
	Output

====================================================================================
Overview
------------------------------------------------------------------------------------
A Windows application to help out with external infrastructure scans.
You can use it for the following:
1.	Convert a file of IP addresses to hostnames (output a straight list of hostnames or comma separated list of IP Address, Hostname)
2.	Convert a file of hostnames to IP addresses (output a straight list of IP Addresses or comma separated list of Hostname , IP Address)
3.	Do a bulk WhoIs lookup on everything in the input file
4.	Run a DNS Zone Recursion and Zone Transfer test against the nameservers associated with the hostnames (it basically takes the work out of it by looking up all the nameservers for you and then running the tests)

====================================================================================
Using HostResolver
------------------------------------------------------------------------------------

Input Files: 
A flat text file of either IP addresses or hostnames, each one on a separate line.

------------------------------------------------------------------------------------

Options and Settings:
	1. Hostname to IP Address - Take a list of hostnames and return the IP addresses.
	2. IP Address to Hostname - Take a list of IP addresses and return the hostnames (where possible)
	3. Include Input Data in Output File - Output can either be a comma separated list of the format [input],[output] or output only (a list of just IP addresses/hostnames)

------------------------------------------------------------------------------------

Scanning and Output:
	1. Host Resolution - Press the 'Start' button to resolve the hosts. Results will be displayed to the output window and written to the output file in the required format.
	2. WhoIs Query - Press the 'WhoIs Query' button to carry out a bulk WhoIs lookup on everything in the input file. Ouput written in text format to an ouput window (copy/paste this output if required)
	3. Name Servers - Press the 'Name Servers' button to carry out a bulk DNS Zone Recursion and Zone Transfer test against the nameservers associated with the hostnames. Ouput written in text format to an ouput window (copy/paste this output if required)

====================================================================================
